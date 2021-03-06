using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    // 달릴때 속도
    public float runSpeed = 6.0f;

    // 걸을때 속도
    public float walkSpeed = 3.0f;

    // 회전할 때 속도
    public float turnSpeed = 5.0f;

    // 이동 모드 지정용 enum
    enum MoveMode
    {
        Walk = 0,
        Run
    }

    // 기본 이동 모드 Run 선택
    MoveMode moveMode = MoveMode.Run;

    // 액션맵 객체
    private PlayerInputActions actions;

    // 
    private CharacterController controller;

    private Vector3 inputDir = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;

    // 애니메이터
    private Animator ani = null;

    private Player player = null;


    private void Awake()
    {
        actions = new();
        controller = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;
        actions.Player.MoveModeChange.performed += OnMoveModeChange;
        actions.Player.Attack.performed += OnAttack;
        actions.Player.LockOn.performed += OnLockOn;
        actions.Player.Pickup.performed += OnPickUp;
        actions.Player.InventoryOnOff.performed += OnInventroyOnOff;
    }
    private void OnDisable()
    {
        actions.Player.InventoryOnOff.performed -= OnInventroyOnOff;
        actions.Player.Pickup.performed -= OnPickUp;
        actions.Player.LockOn.performed -= OnLockOn;
        actions.Player.Attack.performed -= OnAttack;
        actions.Player.MoveModeChange.performed -= OnMoveModeChange;
        actions.Player.Move.canceled -= OnMove;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Disable();
    }
    
    private void OnAttack(InputAction.CallbackContext obj)
    {
        ani.SetFloat("ComboState", Mathf.Repeat(ani.GetCurrentAnimatorStateInfo(0).normalizedTime, 1.0f));
        ani.ResetTrigger("Attack");
        ani.SetTrigger("Attack");

        //GetComponent<Player>().TurnOnAura(true);
    }

    /// <summary>
    /// WASD가 눌러지거나 땠을 때 실행될 함수
    /// </summary>
    /// <param name="context">입력 관련 정보</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        //GetComponent<Player>().TurnOnAura(false);
        // 입력 받은 값 저장
        Vector2 input = context.ReadValue<Vector2>();
        //Debug.Log(input);

        // 입력 받은 값을 3차원 벡터로 변경. (xz평면으로 변환)
        inputDir.x = input.x;   // 오른쪽 왼쪽
        inputDir.y = 0.0f;
        inputDir.z = input.y;   // 앞 뒤
        //inputDir.Normalize();

        // 입력으로 들어온 값이 있는지 확인
        if (inputDir.sqrMagnitude > 0.0f)
        {
            // 카메라의 y축 회전만 따로 분리해서 inputDir에 적용
            inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
            // 이동하는 방향을 바라보는 회전을 생성
            targetRotation = Quaternion.LookRotation(inputDir);
            // 항상 바닥에 붙도록
            inputDir.y = -2.0f;
        }
    }

    /// <summary>
    /// 키입력 들어오면 모드 변경 (Run <=> Walk)
    /// </summary>
    /// <param name="context"></param>
    private void OnMoveModeChange(InputAction.CallbackContext context)
    {
        if(moveMode == MoveMode.Walk)
        {
            moveMode = MoveMode.Run;
        }
        else
        {
            moveMode = MoveMode.Walk;
        }
    }

    /// <summary>
    /// 매 프레임마다 호출
    /// </summary>
    private void Update()
    {
        if(player.lockOnTarget != null)
        {
            targetRotation = Quaternion.LookRotation(player.lockOnTarget.position - transform.position);
        }

        // 이동 입력 확인
        if (inputDir.sqrMagnitude > 0.0f)
        {
            float speed = 0.0f;
            if(moveMode == MoveMode.Run)
            {
                // 런 모드면 달리는 애니메이션과 6의 이동 속도 설정
                ani.SetFloat("Speed", 1.0f);
                speed = runSpeed;
            }
            else if(moveMode == MoveMode.Walk)
            {
                // 걷는 모드면 걷는 애니메이션과 3의 이동 속도 설정
                ani.SetFloat("Speed", 0.3f);
                speed = walkSpeed;
            }

            // 설정한 이동속도에 맞춰 캐릭터 이동
            controller.Move(speed * Time.deltaTime * inputDir);

            // 목표지점을 바라보도록 회전하며 보간
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            // 입력이 없으면 idle 애니메이션으로 변경
            ani.SetFloat("Speed", 0.0f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 락온 버튼이 눌러졌을 때 실행될 함수
    /// </summary>
    /// <param name="_"></param>
    private void OnLockOn(InputAction.CallbackContext _)
    {
        player.LockOnToggle();
    }

    private void OnPickUp(InputAction.CallbackContext obj)
    {
        player.ItemPickUp();
    }
    
    private void OnInventroyOnOff(InputAction.CallbackContext obj)
    {
        GameManager.Inst.InvenUI.InventoryOnOffSwitch();
    }
}
