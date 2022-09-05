using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    public GameObject shellPrefab;
    public Transform firePosition;

    public float moveSpeed = 3.0f;
    public float turnSpeed = 3.0f;
    public float turretTrunSpeed = 3.0f;

    private Transform turret;
    private Quaternion turretTargetRotation = Quaternion.identity;

    private TankInputAction actions;
    private Rigidbody rigidbody;
    private Vector2 input = Vector2.zero;

    private void Awake()
    {
        actions = new();
        rigidbody = GetComponent<Rigidbody>();
        turret = transform.Find("TankRenderers").Find("TankTurret");
        firePosition = turret.GetChild(0);
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;
        actions.Player.Look.performed += OnMouseMove;
        actions.Player.Fire.performed += OnNormalFire;
    }

    private void OnDisable()
    {
        actions.Player.Fire.performed -= OnNormalFire;
        actions.Player.Look.performed -= OnMouseMove;
        actions.Player.Move.canceled -= OnMove;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Enable();
    }
        private void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Ground")))
        {
            //Debug.Log("피킹");
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0.0f;
            lookDir = lookDir.normalized;
            turretTargetRotation = Quaternion.LookRotation(lookDir);

            //turret.LookAt(hit.point);
        }
    }

    private void OnNormalFire(InputAction.CallbackContext _)
    {
        Instantiate(shellPrefab, firePosition.position, firePosition.rotation);
    }

    private void TurrentTurn()
    {
        turret.rotation = Quaternion.Slerp(turret.rotation,turretTargetRotation, turretTrunSpeed * Time.deltaTime);
    }

    private void Update()
    {
        TurrentTurn();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(input.y * moveSpeed * transform.forward, ForceMode.Force);
        rigidbody.AddTorque(input.x * turnSpeed * transform.up, ForceMode.Force);
    }
}
