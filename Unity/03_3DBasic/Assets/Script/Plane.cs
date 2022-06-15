using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public GameObject cone;
    public float TrapSpeed = 5.0f;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Rigidbody rigid = cone.GetComponent<Rigidbody>();

        // inputDir의 x값을 이용하여 이 오브젝트의 오른쪽 방향(transform.forward)으로 이동
        rigid.MovePosition(rigid.position
            + TrapSpeed * Time.fixedDeltaTime
            * inputDir.y * transform.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
