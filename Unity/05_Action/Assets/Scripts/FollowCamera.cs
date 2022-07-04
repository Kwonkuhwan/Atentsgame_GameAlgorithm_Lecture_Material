using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// target을 쫒아다니는 클래스
/// </summary>
public class FollowCamera : MonoBehaviour
{
    // 따라다닐 대상의 트랜스폼.
    public Transform target = null;

    // 따라다니는 속도 (1/speed초에 걸쳐 따라다님)
    public float speed = 3.0f;

    // 처음 타겟과 떨어져 있던 거리
    private Vector3 offset = Vector3.zero;

    private void Start()
    {
        if(target == null)
        {
            target = FindObjectOfType<PlayerInputController>().transform;
        }

        offset = transform.position - target.position;  // 타겟의 위치에서 이 오브젝트로 이동하는 방향 벡터
    }

    // 모든 Update 함수가 실행되고 난 후에 실행
    private void LateUpdate()
    {
        // 보간을 통해 위치를 조정
        transform.position = Vector3.Lerp(
            transform.position,         // 현재 내 위치
            target.position + offset,   // target에 위치에서 (내가 처음 떨어져 있던 간격만큼 떨어져 있는 위치)로 이동
            speed * Time.deltaTime);    // 
    }
}
