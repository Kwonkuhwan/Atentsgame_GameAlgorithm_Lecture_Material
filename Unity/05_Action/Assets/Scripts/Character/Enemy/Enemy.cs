using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Enemy : MonoBehaviour, IHealth, IBattle
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

    // Chase 용 ---------------------------------------------------------------
    private float sightRange = 10.0f;
    private Vector3 targetPosition = new();
    private WaitForSeconds oneSecond = new WaitForSeconds(1);
    private IEnumerator repeatChase;
    private float sightAngle = 150.0f;       // -45 ~ +45도 범위
    // -----------------------------------------------------------------------

    // Attack 용 -------------------------------------------------------------
    float attackCoolTime = 1.0f;
    float attackSpeed = 1.0f;
    // -----------------------------------------------------------------------

    private EnemyState state = EnemyState.Idle;
    private NavMeshAgent agent = null;
    private Animator ani = null;

    // IHealth 용 -------------------------------------------------------------
    public float hp = 100.0f;
    private float maxHP = 100.0f;

    public float HP 
    { 
        get => hp;
        private set
        {
            hp = Mathf.Clamp(value, 0.0f, maxHP);
            onHealthChage?.Invoke();
        }
    }

    public float MaxHP { get => maxHP; }

    public System.Action onHealthChage { get; set; }
    // -----------------------------------------------------------------------

    // IBattle 용 ------------------------------------------------------------
    public float attackPower = 10.0f;
    public float defencePower = 10.0f;
    private float criticalRate = 0.1f;

    public float AttackPower { get => attackPower; }

    public float DefencePower { get => defencePower; }
    // -----------------------------------------------------------------------

    // IDead 용 --------------------------------------------------------------
    private bool isDead = false;
    // -----------------------------------------------------------------------


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
        if (SearchPlayer())
        {
            ChangeState(EnemyState.Chase);
            return;
        }
        timeCountDown -= Time.deltaTime;
        if(timeCountDown < 0)
        {
            ChangeState(EnemyState.Patrol);
            return;
        }
    }

    private void Patrol_Update()
    {
        if (SearchPlayer())
        {
            ChangeState(EnemyState.Chase);
            return;
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            patrolIndex++;
            // patrolIndex %= patrolChildCount;     // 강사님 방법
            if (patrolIndex >= patrolChildCount) 
                patrolIndex = 0;

            ChangeState(EnemyState.Idle);
            return;
        }
    }

    private bool SearchPlayer()
    {
        bool result = false;

        Collider[] colliders =  Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (colliders.Length > 0)
        {
            Vector3 pos = colliders[0].transform.position;
            if (InSightAngle(pos))
            {
                if (!BlockByWall(pos))
                {
                    targetPosition = pos;
                    result = true;
                }
            }
        }

        return result;
    }

    private void Chase_Update()
    {
        if(!SearchPlayer())
        {
            ChangeState(EnemyState.Patrol);
            return;
        }
    }

    private IEnumerator RepeatChase()
    {
        while (true)
        {
            yield return oneSecond;
            agent.SetDestination(targetPosition);
        }
    }

    private void Attack_Update()
    {
        attackCoolTime -= Time.deltaTime;
        if(attackCoolTime < 0.0f)
        {
            ani.SetTrigger("Attack");
            attackCoolTime = attackSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Inst.MainPlayer.gameObject)
        {
            ChangeState(EnemyState.Attack);
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Inst.MainPlayer.gameObject)
        {
            ChangeState(EnemyState.Chase);
            return;
        }
    }

    private void ChangeState(EnemyState newState)
    {
        // 이전 상태를 나가면서 해야할 일들
        switch (state)
        {
            case EnemyState.Idle:
                agent.isStopped = true;
                break;
            case EnemyState.Patrol:
                agent.isStopped = true;
                break;
            case EnemyState.Chase:
                agent.isStopped = true;
                StopCoroutine(repeatChase);
                break;
            case EnemyState.Attack:
                agent.isStopped = true;
                break;
            case EnemyState.Dead:
                isDead = false;
                agent.isStopped = true;
                break;
            default:
                break;
        }

        // 새 상태로 들어가면서 해야할 일들
        switch (newState)
        {
            case EnemyState.Idle:
                agent.isStopped = true;
                timeCountDown = waitTime;
                break;
            case EnemyState.Patrol:
                agent.isStopped = false;
                agent.SetDestination(patrolRoute.GetChild(patrolIndex).position);
                break;
            case EnemyState.Chase:
                agent.isStopped = false;
                agent.SetDestination(targetPosition);                
                repeatChase = RepeatChase();
                StartCoroutine(repeatChase);
                break;
            case EnemyState.Attack:
                agent.isStopped = true;
                attackCoolTime = attackSpeed;
                break;
            case EnemyState.Dead:
                ani.SetBool("Dead", true);
                ani.SetTrigger("Die");
                isDead = true;
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                HP = 0;
                break;
            default:
                break;
        }

        state = newState;
        ani.SetInteger("EnemyState", (int)state);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange);

        Handles.color = Color.green;
        if(state == EnemyState.Chase || state == EnemyState.Attack)
        {
            Handles.color = Color.red;
        }

        Vector3 forward = transform.forward * sightRange;
        Quaternion q1 = Quaternion.Euler(0.5f * sightAngle * transform.up);
        Quaternion q2 = Quaternion.Euler(-0.5f * sightAngle * transform.up);
        Handles.DrawLine(transform.position, transform.position + q1 * forward);
        Handles.DrawLine(transform.position, transform.position + q2 * forward);

        Handles.DrawWireArc(transform.position, transform.up, q2 * transform.forward, sightAngle, sightRange, 5.0f);
        //Handles.DrawLine(transform.position, forward, 3.0f);
    }

    /// <summary>
    /// 플레이어가 시야각고(sightAngle) 안에 있으면 true를 리턴
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    private bool InSightAngle(Vector3 targetPos)
    {
        // 두 벡터의 사이각
        float angle = Vector3.Angle(transform.forward, targetPos - transform.position);
        // 몬스터의 시야범위 각도
        return (sightAngle * 0.5f) > angle;
    }

    private bool BlockByWall(Vector3 targetPos)
    {
        Ray ray = new (transform.position, targetPos - transform.position);
        ray.origin += Vector3.up * 0.5f;
        if (Physics.Raycast(ray, out RaycastHit hit, sightRange))
        {
            if(hit.collider.CompareTag("Player"))
            { 
                return false;
            }
        }

        return true;
    }

    public void Attack(IBattle target)
    {
        if(target != null)
        {
            float damage = AttackPower;
            if(Random.Range(0.0f, 1) < criticalRate)
            {
                damage *= 2.0f;
            }
            target.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage - defencePower;
        if (finalDamage < 1.0f)
        {
            finalDamage = 1.0f;
        }
        HP -= finalDamage;

        if (HP > 0.0f)
        {
            // 살아 있다.
            ani.SetTrigger("Hit");
            attackCoolTime = attackSpeed;
        }
        else
        {
            // 죽었다. 
            Die();
        }
    }

    void Die()
    {
        if (isDead == false)
        {
            ChangeState(EnemyState.Dead);
        }
    }
}
