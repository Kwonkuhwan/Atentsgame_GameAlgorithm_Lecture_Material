//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Player : MonoBehaviour, IHealth, IBattle
{
    private GameObject weapon = null;
    private GameObject sheild = null;

    private ParticleSystem ps = null;
    private Animator ani = null;

    //IHealth ------------------------------------------------------------------------
    public float hp = 100.0f;
    private float maxHP = 100.0f;

    public float HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                onHealthChage?.Invoke();
            }
        }
    }

    public float MaxHP { get => maxHP; }

    public System.Action onHealthChage { get; set; }
    //--------------------------------------------------------------------------------

    //IBattle ------------------------------------------------------------------------
    private float attackPower = 30.0f;
    private float defencePower = 10.0f;
    private float criticalRate = 0.8f;

    public float AttackPower { get => attackPower; }

    public float DefencePower { get => defencePower; }
    //--------------------------------------------------------------------------------

    //LockOn -------------------------------------------------------------------------
    public GameObject lockOnEffect;
    public Transform lockOnTarget;
    float lockOnRange = 5.0f;
    //--------------------------------------------------------------------------------

    //Item ---------------------------------------------------------------------------
    private float itemPickupRange = 3.0f;       // 아이템 줍는 범위
    private int money = 0;                      // 플레이어의 소지 금액
    public int Money 
    { 
        get => money;
        private set
        {
            if (money != value)
            {
                money = value;
                OnMoneyChange?.Invoke(money);
            }
        }    
    }

    public System.Action<int> OnMoneyChange;
    //--------------------------------------------------------------------------------

    private void Awake()
    {
        ani = GetComponent<Animator>();

        weapon = GetComponentInChildren<FindWeapon>().gameObject;
        sheild = GetComponentInChildren<FindShield>().gameObject;

        ps = weapon.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        lockOnEffect = GameObject.Find("LockOnEffect");
    }
    public void ShowWeapons(bool isShow)
    {
        weapon.SetActive(isShow);
        sheild.SetActive(isShow);
    }

    public void TurnOnAura(bool turnOn)
    {
        if (turnOn)
        {
            //Debug.Log("오라 켜기");
            ps.Play();
        }
        else
        {
            //Debug.Log("오라 끄기");
            ps.Stop();
        }
    }

    public void Attack(IBattle target)
    {
        if (target != null)
        {
            float damage = AttackPower;
            if (Random.Range(0.0f, 1) < criticalRate)
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
        }
        else
        {
            // 죽었다. 
            //Die();
        }
    }

    public void LockOnToggle()
    {
        if(lockOnTarget == null)
        {
            // 락온 시도
            LockOn();
        }
        else
        {
            // 락온된 타겟이 있음
            if (!LockOn())  // 다시 락온을 시도
            {
                // 락온 풀기
                LockOff();
            }
        }
    }

    private bool LockOn()
    {
        bool result = false;

        // transform.position 지점에서 반경 lockOnRange 번위 안에 있는 Enemy레이어를 가진 컬라이더를 전부 찾기
        Collider[] cols = Physics.OverlapSphere(transform.position, lockOnRange, LayerMask.GetMask("Enemy"));

        if (cols.Length > 0)
        {

            // 가장 가까운 컬라이더를 찾기
            Collider nearest = null;
            float nearestDistance = float.MaxValue;
            foreach (Collider col in cols)
            {
                float distanceSqr = (col.transform.position - transform.position).sqrMagnitude;
                if (distanceSqr < nearestDistance)
                {
                    nearestDistance = distanceSqr;
                    nearest = col;
                }
            }
            
            lockOnTarget = nearest.transform;
            Debug.Log($"Lock on : { lockOnTarget.name}");

            lockOnTarget.gameObject.GetComponent<Enemy>().OnDead += LockOff;

            lockOnEffect.transform.position = lockOnTarget.position;
            lockOnEffect.transform.parent = lockOnTarget;
            lockOnEffect.SetActive(true);

            result = true;
        }

        return result;
    }

    private void LockOff()
    {
        lockOnTarget = null;
        lockOnEffect.transform.parent = null;
        lockOnEffect.SetActive(false);
    }

    public void ItemPickUp()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, itemPickupRange, LayerMask.GetMask("Item"));
        foreach(var col in cols)
        {
            Item item = col.GetComponent<Item>();
            Money += (int)item.data.value;
            Destroy(col.gameObject);
        }
        //Debug.Log($"{money}");
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.up, lockOnRange);    
    }
}
