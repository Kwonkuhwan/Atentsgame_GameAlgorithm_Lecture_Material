using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int splitCount = 2;              // 쪼개질 개수
    public float splitTime = 3.0f;          // 쪼개질 시간
    public float hit_splitTime = 0.5f;      // 맞아서 쪼개질 시간
    public int hit_Count = 3;               // 맞은 횟수
    public GameObject smail_AsteroidPrefab = null;  // 작은 운석
      
    private void Update()
    {
        // 오일러 앵글 : 3차원 회전을 x축, y축, z축의 합으로 나타낸 것
        // 예시 : (10, 20, 30) => x축으로 10도, y축으로 20도, z축으로 30도
        // 오일러 앵글의 문제점 : 짐벌락이라는 현상이 발생
        // 짐벌락 해결을 위해 쿼터니언(Quaternion, 사원수) 등장 (오일러 앵글에 비해 속도도 빠르고 메모리도 덜 차지함)

        //transform.rotation *= Quaternion.Euler(0, 0, 30.0f * Time.deltaTime); // 1초에 30도씩 돌려라 (반 시계 방향)
        //transform.rotation *= Quaternion.Euler(0, 0, -30.0f * Time.deltaTime); // 1초에 30도씩 돌려라 (시계 방향)
        transform.Rotate(0, 0, 30.0f * Time.deltaTime);
    }

    private void Start()
    {
        StartCoroutine(SplitCoroutine(splitTime));
    }

    IEnumerator SplitCoroutine(float time)      // 코루틴 콜백 함수
    {
        yield return new WaitForSeconds(time);
        SplitAsteroid();
        Destroy(this.gameObject);
    }

    private void SplitAsteroid()
    {
        for (int i = 0; i < splitCount; i++)
        {
            float angle = i * (360.0f / splitCount);            // 사이 각도 구하기
            GameObject obj = Instantiate(smail_AsteroidPrefab); // 작은 운석 생성
            obj.transform.position = transform.position;        // 기준위치(큰 운석)으로 일단 이동
            obj.transform.Rotate(0, 0, angle);                  // 계산한 사이 각도만큼 회전
            obj.transform.position = obj.transform.position + obj.transform.right;  // 서로 떨어진체로 시작 하고 싶을 때
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.tag == "KillZone");      //매우 좋지 않음
        //if (collision.CompareTag("KillZone"))   // 해시 (Hash) -> 유니크한 요약본을 만들어준다.
        //{
        //    // 킬존에 들어갔다.
        //    Destroy(this.gameObject);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit_Count--;
        if(hit_Count <= 0)
        {
            StartCoroutine(SplitCoroutine(hit_splitTime));
        }
    }
}
