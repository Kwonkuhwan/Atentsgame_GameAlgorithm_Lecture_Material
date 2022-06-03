using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Asteroid : MonoBehaviour
{
    public float lifeTime = 3.0f;
    public float speed = 1.0f;
    Rigidbody2D rigid = null;
    SpriteRenderer AsteroidRenderer = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        AsteroidRenderer = GetComponent<SpriteRenderer>();

        int rand = Random.Range(0, 4);
        AsteroidRenderer.flipX = ((rand & 0b_01) != 0);
        AsteroidRenderer.flipY = ((rand & 0b_10) != 0);        
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
