using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP_Bar_UI : MonoBehaviour
{
    private IHealth target;
    private Image fill;

    private void Awake()
    {
        target = GetComponentInParent<IHealth>();
        target.onHealthChage += SetHP_Value;
        fill = transform.Find("Fill").GetComponent<Image>();
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
            fill.fillAmount = ratio;
        }
    }
}
