using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDead
{
    // 플레이어 오브젝트는 입력에 따라 움직인다.
    // w : 전진
    // s : 후진
    // a : 좌회전
    // d : 우회전
    // q : 왼쪽으로 이동
    // e : 오른쪽으로 이동
    // 참조 사이트
    // https://learn.unity.com/project/roll-a-ball?uv=2019.4

    #region public

    public float moveSpeed = 5.0f;
    public float turnSpeed = 180.0f;
    public float jumpPower = 3.0f;

    #endregion

    #region private

    private Player_Input actions = null;
    private Vector3 inputDir = Vector3.zero;
    private float inputSide = 0.0f;

    private Rigidbody rigid = null;

    private Animator ani = null;

    private bool isJumping = false;

    private bool tryUse = false;
    #endregion

    private void Awake()
    {
        actions = new Player_Input();
        rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();

        actions.Player.Move.performed += OnMoveInput;
        actions.Player.Move.canceled += OnMoveInput;

        actions.Player.SideMove.performed += OnSideMoveInput;
        actions.Player.SideMove.canceled += OnSideMoveInput;

        actions.Player.Jump.performed += OnJumpInput;

        actions.Player.Use.performed += OnUseInput;
    }

    private void OnDisable()
    {
        actions.Player.Use.performed -= OnUseInput;

        actions.Player.Jump.performed -= OnJumpInput;

        actions.Player.SideMove.canceled -= OnSideMoveInput;
        actions.Player.SideMove.performed -= OnSideMoveInput;

        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;

        actions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Move();

        Rotate();
    }

    private void Move()
    {
        // inputDir의 x값을 이용하여 이 오브젝트의 오른쪽 방향(transform.forward)으로 이동
        rigid.MovePosition(rigid.position 
            + moveSpeed * Time.fixedDeltaTime 
            * (inputDir.y * transform.forward + inputSide * transform.right));
    }

    private void Rotate()
    {
        rigid.MoveRotation(rigid.rotation * Quaternion.AngleAxis(inputDir.x * turnSpeed * Time.fixedDeltaTime, transform.up));
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        inputDir = context.ReadValue<Vector2>();    // Vector2.x = a키(-1), d키(+1)  Vector2.y = w키(+1), s키(-1)
    
        ani.SetBool("IsMove", !context.canceled);
    }

    private void OnSideMoveInput(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());
        inputSide = context.ReadValue<float>();

        ani.SetBool("IsMove", !context.canceled);
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void Use()
    {
        ani.SetTrigger("Use");
        // 트리거를 어떻게 사용하면 될 것인가?

        tryUse = true;
    }

    public void UseEnd()
    {
        tryUse = false;
    }

    private void OnUseInput(InputAction.CallbackContext context)
    {
        Use();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("무엇인가가 들어왔다.");

        // 사용이 가능한지 아닌지 물어보기(useable이 null이 아니라면 IUseable인터페이스를 상속 받았기에 사용할 수 있는 오브젝트다)

        if (tryUse)
        {
            IUseable useable = other.GetComponentInParent<IUseable>();
            if (useable != null)
            {
                useable.Use();  // 사용할 수 있는 오브젝트라면 사용한다.
                tryUse = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("무엇인가가 나갔다.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    public void Die()
    {
        actions.Player.Disable();
        rigid.constraints = RigidbodyConstraints.None;
        rigid.AddForce(Random.insideUnitSphere * 10.0f);
        ani.SetTrigger("Die");
        StartCoroutine(Restart());  // 죽을 때 씬 재시작 코루틴 실행
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(5.0f);   // 5초 기다리기
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // 현재 씬 다시 불러오기
    }
}
