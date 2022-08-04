using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터를 저장하는 데이터 파일을 만들게 해주는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New Weapon Item Potion", menuName = "Scriptable Object/Item Data - Weapon", order = 5)]
public class ItemData_Weapon : ItemData, IEquipItem
{
    bool isEquiped = false;

    public bool IsEqiped => isEquiped;

    public void EquipItem(IEquipTarget target)
    {
        target.EqupWeapon(this);
        isEquiped = true;
    }

    public void ToggleEquipItem(IEquipTarget target)
    {
        if(target.IsWeaponEquiped)
        {
            target.UnEqupWeapon();
        }
        else
        {
            target.EqupWeapon(this);
        }
    }

    public void UnEquipItem(IEquipTarget target)
    {
        target.UnEqupWeapon();
    }
}
