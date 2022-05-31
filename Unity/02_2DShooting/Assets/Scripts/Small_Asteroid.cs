using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Asteroid : MonoBehaviour
{
    public float lifeTime = 3.0f;
    public float speed = 1.0f;
    Rigidbody2D rigid = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid.velocity = transform.right * speed;
        StartCoroutine(LifeDelay());
    }

    IEnumerator LifeDelay()      // 코루틴 콜백 함수
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
