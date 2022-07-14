using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP_Bar : MonoBehaviour
{
    private IHealth target;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        target = GameManager.Inst.MainPlayer.GetComponent<IHealth>();
        target.onHealthChage += SetHP_Value;

    }

    private void SetHP_Value()
    {
        if (target != null)
        {
            float ratio = target.HP / target.MaxHP;
            slider.value = ratio;
        }
    }
}
