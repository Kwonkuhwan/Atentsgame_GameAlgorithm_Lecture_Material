using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shell : MonoBehaviour
{
    private float initialSpeed = 3.0f;
    public GameObject ExplosionPrefab;

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigid.velocity = transform.forward * initialSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionPrefab, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(this.gameObject);
    }
}
