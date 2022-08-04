using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipItem
{
    bool IsEqiped { get; }

    void EquipItem(IEquipTarget target);
    void UnEquipItem(IEquipTarget target);
    void ToggleEquipItem(IEquipTarget target);
}
