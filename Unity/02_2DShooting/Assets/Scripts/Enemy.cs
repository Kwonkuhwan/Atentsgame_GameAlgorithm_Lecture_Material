using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 3.0f;
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
}
