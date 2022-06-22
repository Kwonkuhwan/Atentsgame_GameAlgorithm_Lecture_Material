using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // 타켓
    public Transform target = null;
    // 총알
    public GameObject bullet = null;
    // 인식범위
    public float range = 5.0f;
    // 발사각
    public float angle = 15.0f;
    public float fireInterval = 1.0f;

    // 헤드
    private Transform turretHead = null;
    private Transform firePosition = null;
    // 추적 속도
    private float lookSpeed = 2.0f;
    private float halfAngle = 0.0f;
    private float fireCooltime = 0.0f;

    private void Awake()
    {
        turretHead = transform.Find("Head");
        firePosition = turretHead.Find("FirePosition");
        halfAngle = angle * 0.5f;   // 나누기를 하는 것보다 곱하기가 좋다. 성능면에서 차이가 난다.
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position; // 포탑에서 플레이어로 향하는 방향 벡터
        // dir의 길이 = root(dir.x * dir.x + dir.y * dir.y + dir.z * dir.z)
        // dir의 길이 = dir.magnitude;

        fireCooltime -= Time.deltaTime;

        if(dir.sqrMagnitude < range * range)
        {
            // 포탑의 range 안이다.
            //Vector3 targetPos = target.position;
            //targetPos.y = turretHead.position.y;
            //turretHead.LookAt(targetPos);
            turretHead.rotation = Quaternion.Lerp(                    // 보간함수. (시작값, 도착값, 시간) 3가지를 받아서 계산된 결과를 돌려준다.
                                                                      // 시간이 0이면 시작값, 시간이 1이면 도착값, 시간이 0~1사이면 비율에 맞춰서
                turretHead.rotation,            // 시작값. (현재 포탑머리의 회전)
                Quaternion.LookRotation(dir),   // 도착값. (dir방향으로 바라보는 회전)
                lookSpeed * Time.deltaTime);    // 1초동안 모으면 2가 된다. 0 -> 1로 가는데 걸리는 시간은 0.5초가 된다. => 시작에서 도착까지

            float angleBetween = Vector3.SignedAngle(turretHead.forward, dir, Vector3.up);
            //Debug.Log($"angleBetween : {angleBetween}");

            if(angleBetween < halfAngle)
            {
                //Debug.Log($"Fire!!!");
                if (fireCooltime < 0.0f)
                {
                    Fire();
                    fireCooltime = fireInterval;
                }
            }
        }
    }

    private void Fire()
    {
        Instantiate(bullet, firePosition.position, firePosition.rotation);
    }
}
