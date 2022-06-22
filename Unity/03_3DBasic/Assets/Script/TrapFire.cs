using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    private ParticleSystem particle = null;     // 파티클은 점 1개. 거게이 텍스쳐를 붙임. CPU가 처리

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     // 트리거에 들어온 것이 플레이어면
        {
            particle.Play();                // 파티클 재생 시키고

            IDead dead = other.GetComponent<IDead>();
            dead?.Die();                    // 죽을 수 있으면 죽인다.
        }
    }
}
