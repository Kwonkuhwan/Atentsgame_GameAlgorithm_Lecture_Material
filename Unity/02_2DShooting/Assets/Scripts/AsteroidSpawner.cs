using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : EnemySpawner
{
    private void Awake()
    {
        //GameObject obj = GameObject.Find("이름");                         // 이름으로 찾기. 가장 비효율적.
        //GameObject obj = GameObject.FindGameObjectWithTag("태그이름");    // 태그로 찾기. 이것도 씬 전체를 확인
        //타입 obj = GameObject.FindObjectOfType<타입>();                   // 타입으로 찾기. 씬 전체를 확인

        target = transform.Find("Target");  //비효율적이지만 자식의 수는 보통 얼마되지 않기 때문에 그냥 사용하는 것
        start = transform.Find("Start");  //비효율적이지만 자식의 수는 보통 얼마되지 않기 때문에 그냥 사용하는 것
    }

    protected override IEnumerator Spawn()
    {
        while (true)
        {
            yield return waitSecond;
            GameObject obj = Instantiate(EnemyPrefab);
            obj.transform.position = transform.position;
            startPosition = Vector3.up * Random.Range(0.0f, randomRange);
            obj.transform.Translate(startPosition);
            startPosition = startPosition + transform.position;

            // 도착지점을 랜덤으로 정하기
            endPosition = target.transform.position + Vector3.up * Random.Range(0.0f, targetLength); // 타켓 위치 + 위쪽방향
            Asteroid asteroid = obj.GetComponent<Asteroid>();   // 생성한 obj에서 asteroid 컴포넌트 가져오기
            asteroid.targetDir = (endPosition - obj.transform.position).normalized;

            // 도착지점을 향하도록 회전하기
            //float angle = Vector3.SignedAngle(Vector3.left, toPosition - transform.position, Vector3.forward);
            //obj.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawLine(target.position, target.position + Vector3.up * targetLength);
        Gizmos.DrawLine(start.position, transform.position + Vector3.up * targetLength);
    }
}
