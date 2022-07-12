using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP_Bar : MonoBehaviour
{
    private IHealth target;
    private Transform fillPivot;

    private void Awake()
    {
        target = GetComponentInParent<IHealth>();
        target.onHealthChage += SetHP_Value;
        fillPivot = transform.Find("FillPivot");
    }

    private void LateUpdate()
    {
        //transform.LookAt(transform.position + Camera.main.transform.rotation - transform.position);
        transform.forward = -Camera.main.transform.position;
    }

    private void SetHP_Value()
    {
        if(target != null)
        {
            float ratio = target.HP / target.MaxHP;
            fillPivot.localScale = new(ratio, 1, 1);
        }
    }
}
