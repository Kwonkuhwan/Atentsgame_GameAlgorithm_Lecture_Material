using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMP_Bar : MonoBehaviour
{
    private IMana target;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        target = GameManager.Inst.MainPlayer.GetComponent<IMana>();
        target.onManaChage += SetMP_Value;
        SetMP_Value();
    }

    private void SetMP_Value()
    {
        if (target != null)
        {
            float ratio = target.MP / target.MaxMP;
            slider.value = ratio;
        }
    }
}
