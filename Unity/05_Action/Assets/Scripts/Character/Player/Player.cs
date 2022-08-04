//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Player : MonoBehaviour, IHealth, IMana, IBattle, IEquipTarget
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
                hp = Mathf.Clamp(value, 0, maxHP);
                onHealthChage?.Invoke();
            }
        }
    }

    public float MaxHP { get => maxHP; }

    public System.Action onHealthChage { get; set; }
    //--------------------------------------------------------------------------------

    //IMana ------------------------------------------------------------------------
    public float mp = 150.0f;
    private float maxMP = 150.0f;

    public float MP 
    {
        get => mp;
        set
        {
            if(mp != value)
            {
                mp = Mathf.Clamp(value, 0, maxMP);
                onManaChage?.Invoke();
            }
        }
    }

    public float MaxMP { get => maxMP; }

    public System.Action onManaChage { get; set; }
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
    private float lockOnRange = 5.0f;
    //--------------------------------------------------------------------------------

    //Item ---------------------------------------------------------------------------
    private float itemPickupRange = 3.0f;       // 아이템 줍는 범위
    private int money = 0;                      // 플레이어의 소지 금액
    public int Money 
    { 
        get => money;
        set
        {
            if (money != value)
            {
                money = value;
                OnMoneyChange?.Invoke(money);
            }
        }    
    }

    ItemData_Weapon equipItem;
    public ItemData_Weapon EquipItem => equipItem;

    public bool IsWeaponEquiped => equipItem != null;

    public System.Action<int> OnMoneyChange;
    private float dropRange = 2.0f;
    //--------------------------------------------------------------------------------

    //Inventory ----------------------------------------------------------------------
    private Inventory inven;
    //--------------------------------------------------------------------------------


    private void Awake()
    {
        ani = GetComponent<Animator>();

        weapon = GetComponentInChildren<FindWeapon>().gameObject;
        sheild = GetComponentInChildren<FindShield>().gameObject;

        ps = weapon.GetComponentInChildren<ParticleSystem>();

        inven = new Inventory();
    }

    private void Start()
    {
        if (lockOnEffect == null)
        {
            lockOnEffect = GameObject.Find("LockOnEffect");
        }
        GameManager.Inst.InvenUI.InitializeInventory(inven);
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
            //Debug.Log($"Lock on : { lockOnTarget.name}");

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
            IConsumalbe consumable = item.data as IConsumalbe;
            if(consumable != null)
            {
                consumable.Consume(this);
                Destroy(col.gameObject);
            }
            else
            {
                if(inven.AddItem(item.data))
                {
                    Destroy(col.gameObject);
                }
            }
        }
        //Debug.Log($"{money}");
    }

    public Vector3 ItemDropPosition(Vector3 inputPos)
    {
        Vector3 result = Vector3.zero;
        Vector3 toInputPos = inputPos - transform.position;
        if(toInputPos.sqrMagnitude > dropRange * dropRange)
        {
            result = transform.position + toInputPos.normalized * dropRange;
        }
        else
        {
            result = inputPos;
        }

        return result;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.up, lockOnRange);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, itemPickupRange);
    }

    public void EquipWeapon(ItemData_Weapon weaponData)
    {
        ShowWeapons(true);      // 장비하면 무조건 보이도록
        GameObject obj = Instantiate(weaponData.prefab, weapon.transform);  // 새로 장비할 아이템 생성
        obj.transform.localPosition = new(0, 0, 0);                         // 부모에게 정확히 붙도록 로컬을 0,0,0으로 설정
        ps = obj.GetComponent<ParticleSystem>();                            // 파티클 시스템 갱신
        equipItem = weaponData;                                             // 장비한 아이템 표시
    }

    public void UnEquipWeapon()
    {
        equipItem = null;                                   // 장비가 해제됬다는 것을 표시하기 위함(IsWeaponEquiped 변경용)
        ps = null;                                          // 파티클 시스템 비우기
        Transform sword = weapon.transform.GetChild(0);
        sword.parent = null;
        Destroy(sword.gameObject);
    }

}
