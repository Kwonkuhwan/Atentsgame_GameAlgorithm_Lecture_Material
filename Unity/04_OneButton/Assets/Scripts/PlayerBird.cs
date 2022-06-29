using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBird : MonoBehaviour
{
    public float power = 5.0f;                          // 위로 올리는 힘
    public float pitchMaxAngle = 30.0f;
    
    private Bird_Actions birdInputAction = null;        // 액션맵 파일을 기반으로 자동생성된 클래스(Bird_Actions.cs)
    private Rigidbody2D rigid = null;                   // 2D용 리지드 바디

    private bool isDead = false;

    private void Awake()
    {
        birdInputAction = new();                        // Bird_Actions를 새롭게 new
        rigid = GetComponent<Rigidbody2D>();            // 리지드바디 캐싱 해놓기
    }

    private void OnEnable()
    {
        birdInputAction.Player.Enable();                // 스크립트로 InputSystem을 제어할 때는 활성화/비활성화를 수동으로 추가
        birdInputAction.Player.Tab.performed += OnTab;  // Tab 액션이 발동될 때 실행될 함수 등록
    }

    private void OnDisable()
    {
        birdInputAction.Player.Tab.performed -= OnTab;  // Tab 액션이 발동될 때 실행될 함수 등록해제
    birdInputAction.Player.Disable();
    }

    private void OnTab(InputAction.CallbackContext _)
    {
        //Debug.Log("Tab");
        rigid.velocity = Vector2.zero;                  // 이전에 영향받고 있던 움직임 초기화
        rigid.AddForce(Vector2.up * power, ForceMode2D.Impulse);    // 위쪽으로 힘을 추가

        // velocity y는 power가 5이니 5~-5로 주자
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            // velocity y는 +5(1) ~ -5(0)
            // 각도 +45(1) ~ -45(0)
            // 시작값과 끝값이 있다. => 보간
            // 정규화 : 0 ~ 1

            float vel = Mathf.Clamp(rigid.velocity.y, -power, power);   // -5 ~ 5
                                                                        //float velNormalized = (vel + power) / (power * 2.0f);          // 0 ~ 1
                                                                        //float angle = (velNormalized * (pitchMaxAngle * 2.0f)) - pitchMaxAngle;    // -30 ~ 30

            float angle = vel * pitchMaxAngle / power;  // velocity.y * 30도 / 5

            rigid.MoveRotation(angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("충돌");
        Die(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!isDead)
        {
            GameManager.Inst.Score += GameManager.Inst.point;
        }
    }

    void Die(Collision2D collision)
    {
        isDead = true; // 죽었다고 표시

        //Destroy(this.gameObject);
        birdInputAction.Player.Disable();   // 입력 막기

        ContactPoint2D contact = collision.GetContact(0);   // 충돌 지점
        //contact.normal  // 노멀벡터 : 평면에 수직인 벡터(법선벡터)

        // 새의 위치에서 부딪힌 위치로 가는 방향 벡터의 정규화된 벡터
        Vector2 dir = (contact.point - (Vector2)transform.position).normalized;
        // dir이 contact.normal를 노멀로 가지는 평면에 부딪쳐서 반사된 벡터
        Vector2 reflect = Vector2.Reflect(dir, contact.normal);
        rigid.velocity = reflect * 10.5f + Vector2.left * 5.0f; // 반사벡터 + 왼쪽으로 튕겨내기

        rigid.angularVelocity = 1000.0f;    // 회전 시키기(1초에 1000도)
    }
}
