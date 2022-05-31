using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 값타입(Value type : 실제 값을 가지는 타입 int, float, bool)
    // 참조타입(reference type : 각정 클래스들을 new 한 것들을 담을 수 있는 타입, 메모리 주소 같은 것들을 저장하는 타입)
    // null은 참초타입의 변수가 비어있다고 표시하는 키워드
    // var는 컴파일타입에 변수의 타입을 결정해주는 키워드이다.

    // public 변수는 인스펙터 창에서 확인 할 수 있다.
    public GameObject ShootPrefab = null;
    public Transform[] FirePosition = null;

    public float f_moveSpeed = 2.0f;
    public float f_boostSpeed = 1.0f;

    private Vector3 direction = Vector3.zero;
    private Rigidbody2D rigid = null;       // 계속 사용할 컴포넌트는 한번만 찾는게 좋다.
   
    private IEnumerator shoot_coroutine = null;

    private void Awake()        // 게임 오브젝트가 만들어진 직후에 호출
    {
        rigid = GetComponent<Rigidbody2D>();

        // Unity는 안 움직이는 Collider는 하나로 합친 후 움직이는 Collider와 충돌처리를 계산한다.
        // Unity는 Rigidbody가 있는 오브젝트만 움직인 오브젝트로 판단한다.
        // Rigidbody가 없는 Collider가 움직이게 되면 다음 프레임에 다시 Collider를 합치기 때문에
        // 이런 동작이 반복되면 최적화에 심각한 악영향을 미친다.
    }

    // Start is called before the first frame update => 게임이 시작되었을 때 Start가 호출됩니다.(첫번째 Update가 실행되기 전에)
    void Start()
    {
        shoot_coroutine = ShootCoroutine();
    }

    // Update is called once per frame => 게임이 실행되는 도중에 주기적으로 호출된다.
    void Update()
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

        #region input manager, input system example
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
        #endregion

    }

    private void FixedUpdate()
    {
        // 물리 연산은 정확한 시잔 간격으로 실행해야 정확한 결과가 나온다.
        // 그런데 Update함수는 항상 똑같은 시간 간격으로 실행되지 않는다.
        // => 일반 Update에서는 물리 연산에 오류가 있을 수 있다.
        // FixedUpdate는 설정에 지정되어 있는 일정한 시간간격으로 항상 호출된다.
        // Rigidbody를 가지는 오브젝트를 움직일(이동, 회전 등) 때는 FixedUpdate 안에서 해야 한다.
        Move();
    }

    private void Move()
    {
        // transform의 position 변경
        //transform.position += (direction.normalized * moveSpeed * Time.deltaTime);
        //transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        //transform.Translate(1 * Time.deltaTime, 0, 0); // 계속 오른쪽으로 이동하는 코드

        // Rigidbody를 이용해서 이동
        rigid.MovePosition(transform.position + (direction.normalized * f_moveSpeed * f_boostSpeed * Time.fixedDeltaTime));
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {       
        direction = context.ReadValue<Vector2>();
    }
    public void OnBoostInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            f_boostSpeed = 2.0f;
        }
        else if (context.canceled)
        {
            f_boostSpeed = 1.0f;
        }
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        //if(context.started)         // 키를 누르기 시작했을때(키보드에서는 started와 performed의 차이가 없다)
        //{
        //    Debug.Log("OnFire - 시작");
        //}
        //else if (context.performed)  // 키를 완전히 눌렀을때
        //{
        //    Debug.Log("OnFire - 완전히 누름");
        //}
        //else if (context.canceled)  // 키를 땠을때
        //{
        //    Debug.Log("OnFire - 키보드 땠음");
        //}

        if (context.started)
        {
            //    // 3줄 발사
            //    for (int i = 0; i < 3; i++)
            //    {
            //        GameObject obj = Instantiate(ShootPrefab);                                  // 총알 생성
            //        obj.transform.position = transform.position + (transform.right * 1.2f) + (transform.up * (i-1) * 0.4f);       // 플레이어 오른쪽으로 1.2만큼 떨어진 위치에 배치           
            //        obj.transform.rotation = transform.rotation;                                // 플레이어의 회전을 그대로 적용
            //    }

            // 위치 표시용 빈 게임 오브젝트를 이용해 총알 발사(여러개 가능)
            //for (int i = 0; i < FirePosition.Length; i++)
            //{
            //    GameObject obj = Instantiate(ShootPrefab);
            //    //obj.transform.parent = null;        // obj의 부모를 제거하기
            //    obj.transform.position = FirePosition[i].position;
            //    obj.transform.rotation = FirePosition[i].rotation;
            //}

            //Debug.Log("b_stop_Shoot_coroutine = fasle");
            StartCoroutine(shoot_coroutine);
        }
        else if (context.canceled)
        {
            //Debug.Log("b_stop_Shoot_coroutine = true");
            StopCoroutine(shoot_coroutine);
        }
    }

    IEnumerator ShootCoroutine()                                 // 코루틴 정의
    {
        //yield return new WaitForSeconds(1.0f);                  // 1초 대기

        while (true)
        {
            for (int i = 0; i < FirePosition.Length; i++)
            {
                GameObject obj = Instantiate(ShootPrefab);
                //obj.transform.parent = null;        // obj의 부모를 제거하기
                obj.transform.position = FirePosition[i].position;
                obj.transform.rotation = FirePosition[i].rotation;
            }
            yield return new WaitForSeconds(0.5f);              // 0.2초 대기
        }
    }
}
