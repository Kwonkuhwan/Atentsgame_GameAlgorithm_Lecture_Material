using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestWeaponSwing : MonoBehaviour
{
    // polling : 데이터를 당겨오는 것. 계속 데이터의 변화를 확인하다가 원하는 상태가 되면 당겨오는 처리.

    private bool movingStart = false;
    private float speed = 180.0f;
    private float angle = 0.0f;

    private Animator ani = null;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //Debug.Log("Space!");
            movingStart = true;
            //ani.SetTrigger("Swing");
        }

        if (movingStart)
        {
            angle += (speed * Time.deltaTime);
            if (angle > 360.0f)
            {
                movingStart = false;
                angle = 0.0f;
            }
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
            //transform.Rotate(Vector3.up * 180.0f * Time.deltaTime);
    }
}
