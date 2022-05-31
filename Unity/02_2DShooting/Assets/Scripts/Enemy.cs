using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 10.0f;
    Rigidbody2D rigid = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rigid.velocity = ((-1) * transform.right) * speed;
        StartCoroutine(LifeDelay());
    }

    IEnumerator LifeDelay()      // 코루틴 콜백 함수
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    // 이 스크립트를 가지고 있는 게임 오브젝트의 컬라이더가 다른 트리거 안에 들어가야 실행
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Debug.Log($"OnTriggerEnter2D : {collision.gameObject.name}");
    //    //if(collision.tag == "KillZone");      //매우 좋지 않음
    //    //if (collision.CompareTag("KillZone"))   // 해시 (Hash) -> 유니크한 요약본을 만들어준다.
    //    //{
    //    //    // 킬존에 들어갔다.
    //    //    Destroy(this.gameObject);
    //    //}
    //}

    // 이 스크립트를 가지고 있는 게임 오브젝트의 컬라이더가 다른 컬라이더와 부딪혀야 실행
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Bullet"))   // 해시 (Hash) -> 유니크한 요약본을 만들어준다.
        //{
            Destroy(this.gameObject);
        //}
    }
}
