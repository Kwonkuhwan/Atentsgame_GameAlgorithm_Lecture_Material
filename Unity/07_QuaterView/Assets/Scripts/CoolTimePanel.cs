using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTimePanel : MonoBehaviour
{
    CoolTimeSlot[] coolTimeSlots;
    public CoolTimeSlot this[int index]
    {
        get => coolTimeSlots[index];
    }

    public int slotLength { get => coolTimeSlots.Length; }

    private void Awake()
    {
        coolTimeSlots = GetComponentsInChildren<CoolTimeSlot>();
    }

    public void SetSelected(int select)
    {
        if (select == 0)
        {
            coolTimeSlots[select + 1].SetSelected(true);
            coolTimeSlots[select + 2].SetSelected(false);
        }
        else
        {
            coolTimeSlots[select].SetSelected(false);
            coolTimeSlots[select + 1].SetSelected(true);
        }
    }
}