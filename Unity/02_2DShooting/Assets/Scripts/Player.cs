using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Vector3 direction = new Vector3();

    // Start is called before the first frame update => 게임이 시작되었을 때 Start가 호출됩니다.
    //void Start()
    //{        
    //}

    // Update is called once per frame => 게임이 실행되는 도중에 계속 호출된다.
    void Update()
    {
        //OnKeyDwon();        
    }

    private void OnKeyDwon()
    {
        // transform 접근 방식 3개가 전부 가능
        // this.gameObject.transform;
        // gameObject.transform;
        // transform;

        // Vector3 : float 타입의 x, y, z를 가지는 구조체
        // Vector3 a = new Vector3(1, 2, 3);        // a.x = 1,  a.y = 2,  a.z = 3
        // Vector3 b = new Vector3(10, 20, 30);     // b.x = 10, b.y = 20, b.z = 30

        // Time.deltaTime; // 이전 프레임에서 지금 프레임까지 걸린 시간
        // Update 함수안에서 (moveSpeed * Time.deltaTime)은 1초에 moveSpeed 만큼 이라는 의미가 된다.
        float MoveSpeed = moveSpeed * Time.deltaTime;

        /**************************************************************************************************************/

        // input manager
        //if (Input.GetKey(KeyCode.RightArrow) == true)
        // transform.position = transform.position + new Vector3(MoveSpeed, 0.0f, 0.0f);
        // transform.position = transform.position + Vector3.right * MoveSpeed;

        /**************************************************************************************************************/

        // input system

        // 왼쪽
        //if (Keyboard.current.aKey.ReadValue() > 0.0f) { direction.x = -1.0f;}
        // 오른쪽
        //else if (Keyboard.current.dKey.ReadValue() > 0.0f) { direction.x = 1.0f; }
        //else { direction.x = 0; }

        // 아래 
        //if (Keyboard.current.sKey.ReadValue() > 0.0f) { direction.y = -1.0f; }
        // 위
        //else if (Keyboard.current.wKey.ReadValue() > 0.0f) { direction.y = 1.0f; }
        //else { direction.y = 0; }

        // 벡터의 정규화 : 벡터의 크기를 1로 만드는 작업. 벡터에서 순수하게 방향만 남기는 작업. 벡터의 x,y,z를 벡터의 길이로 나누면 된다.
        // 단위 벡터 : 벡터를 정규화한 결과. 길이가 1인 벡터
        //transform.position = transform.position + direction.normalized * MoveSpeed;

        /**************************************************************************************************************/
    }

    public void OnMove(InputAction.CallbackContext context)
    {

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("OnFire 동작");
    }
}
