using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))   // 플레이어면
        {
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();   // 리지드 바디 가져와서
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);          // 위쪽으로 힘들 가해라
        }
    }
}
