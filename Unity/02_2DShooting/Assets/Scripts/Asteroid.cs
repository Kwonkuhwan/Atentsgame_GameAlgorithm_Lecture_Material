using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int splitCount = 2;              // 쪼개질 개수
    public float splitTime = 3.0f;          // 쪼개질 시간
    public float hit_splitTime = 0.5f;      // 맞아서 쪼개질 시간
    public int hit_Count = 3;               // 맞은 횟수
    public float moveSpeed = 1.0f;
    public GameObject smail_AsteroidPrefab = null;  // 작은 운석

    public Vector3 targetDir = Vector3.zero;

    public int broken_score = 10;

    private SpriteRenderer AsteroidRenderer = null;
    private void Awake()
    {
        AsteroidRenderer = GetComponent<SpriteRenderer>();

        int rand = Random.Range(0, 4);
        AsteroidRenderer.flipX = ((rand & 0b_01) != 0);
        AsteroidRenderer.flipY = ((rand & 0b_10) != 0);
    }

    private void Update()
    {
        // 오일러 앵글 : 3차원 회전을 x축, y축, z축의 합으로 나타낸 것
        // 예시 : (10, 20, 30) => x축으로 10도, y축으로 20도, z축으로 30도
        // 오일러 앵글의 문제점 : 짐벌락이라는 현상이 발생
        // 짐벌락 해결을 위해 쿼터니언(Quaternion, 사원수) 등장 (오일러 앵글에 비해 속도도 빠르고 메모리도 덜 차지함)

        //transform.rotation *= Quaternion.Euler(0, 0, 30.0f * Time.deltaTime); // 1초에 30도씩 돌려라 (반 시계 방향)
        //transform.rotation *= Quaternion.Euler(0, 0, -30.0f * Time.deltaTime); // 1초에 30도씩 돌려라 (시계 방향)
        transform.Rotate(0, 0, 30.0f * Time.deltaTime);
        transform.Translate(targetDir * moveSpeed * Time.deltaTime, Space.World);
        //transform.position += (Vector3)(Vector2.left* moveSpeed * Time.deltaTime);


        // 움직임
        // transform을 이용해서 움직이는 것 : 물리같은 것 고려 없음. 그냥 무조건 지정된 위치 변경(텔레포트)
        //  - position으로 움직이는 것
        //  - Translate로 움직이는 것

        // Rigidbody를 이용해서 움직이는 것 : 물리 고려(운동량, 속도, 질량). MovePosition(텔레포트는 텔레포트인데 물체의 충돌영역은 고려함)
    
        // 월드 좌표계(World Coordinate System)
        // - 월드(맵, 씬)의 원점을 기준으로 만들어진 좌표계
        // 로컬 좌표계(Local Corrdinate System)
        // - 각 오브젝트의 중심점(Pivot)을 기준으로 만들어진 좌표계
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
            GameManager.Inst.Score += broken_score;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + targetDir * 1.5f); ;
    }
}
