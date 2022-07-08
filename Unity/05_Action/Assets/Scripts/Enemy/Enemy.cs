using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // patrol 용 --------------------------------------------------------------
    public Transform patrolRoute = null;
    private int patrolChildCount = 0;
    private int patrolIndex = 0;
    // -----------------------------------------------------------------------

    // Idle 용 ----------------------------------------------------------------
    private float waitTime = 3.0f;
    private float timeCountDown = 3.0f;
    // -----------------------------------------------------------------------

    private EnemyState state = EnemyState.Idle;
    private NavMeshAgent agent = null;
    private Animator ani = null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        if (patrolRoute)
        {
            patrolChildCount = patrolRoute.childCount;
        }
    }

    private void Update()
    {
        //if (partrolRoute != null)
        //{
        //    agent.SetDestination(partrolRoute.position);  // 길찾기는 연산량이 많은 작업. SetDestination을 자주하면 안된다.
        //}

        //if((transform.position - patrolRoute.GetChild(0).position).sqrMagnitude < 0.1f)
        //{
        //    Debug.Log("도착");
        //}

        switch (state)
        {
            case EnemyState.Idle:
                Idle_Update();
                break;
            case EnemyState.Patrol:
                Patrol_Update();
                break;
            case EnemyState.Chase:
                Chase_Update();
                break;
            case EnemyState.Attack:
                Attack_Update();
                break;
            case EnemyState.Dead:
            default:
                break;
        }
    }

    private void Idle_Update()
    {
        timeCountDown -= Time.deltaTime;
        if(timeCountDown < 0)
        {
            ChangeState(EnemyState.Patrol);
        }
    }
    private void Patrol_Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            patrolIndex++;
            // patrolIndex %= patrolChildCount;     // 강사님 방법
            if (patrolIndex >= patrolChildCount)
            {
                patrolIndex = 0;
            }
            ChangeState(EnemyState.Idle);
        }
    }

    private void Chase_Update()
    {

    }

    private void Attack_Update()
    {

    }

    private void ChangeState(EnemyState newState)
    {
        // 이전 상태를 나가면서 해야할 일들
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrol:
                agent.isStopped = true;
                break;
            case EnemyState.Chase:
                agent.isStopped = true;
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Dead:
                break;
            default:
                break;
        }

        // 새 상태로 들어가면서 해야할 일들
        switch (newState)
        {
            case EnemyState.Idle:
                timeCountDown = waitTime;
                break;
            case EnemyState.Patrol:
                agent.isStopped = false;
                agent.SetDestination(patrolRoute.GetChild(patrolIndex).position);
                break;
            case EnemyState.Chase:
                agent.isStopped = false;
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Dead:
                break;
            default:
                break;
        }

        state = newState;
        ani.SetInteger("EnemyState", (int)state);
    }
}
