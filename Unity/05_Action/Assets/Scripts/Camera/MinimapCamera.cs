using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// target을 쫒아다니는 클래스
/// </summary>
public class MinimapCamera : MonoBehaviour
{
    // 따라다닐 대상의 트랜스폼.
    public Transform target = null;

    // 모든 Update 함수가 실행되고 난 후에 실행
    private void LateUpdate()
    {
        transform.position = target.position + Vector3.up * 10;
    }
}
