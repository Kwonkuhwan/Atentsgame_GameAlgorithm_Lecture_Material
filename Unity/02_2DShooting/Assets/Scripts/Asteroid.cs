using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int splitCount = 2;
    public float splitTime = 3.0f;
    public GameObject smail_AsteroidPrefab = null;

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
        StartCoroutine(SplitCoroutine());
    }

    IEnumerator SplitCoroutine()      // 코루틴 콜백 함수
    {
        yield return new WaitForSeconds(splitTime);
        SplitAsteroid();
        Destroy(this.gameObject);
    }

    private void SplitAsteroid()
    {
        for (int i = 0; i < splitCount; i++)
        {
            GameObject obj = Instantiate(smail_AsteroidPrefab);
            obj.transform.position = transform.position;
            obj.transform.Rotate(0, 0, i * (360.0f / splitCount));

            obj.transform.Translate(1 * Time.deltaTime, 0, 0); // 계속 오른쪽으로 이동하는 코드
        }

    }
}
