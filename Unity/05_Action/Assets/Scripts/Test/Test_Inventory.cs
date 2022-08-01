using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    private void Start()
    {
        //Test_AddRemoveMove();

        Inventory inven = new Inventory();

        InventoryUI invenUI = FindObjectOfType<InventoryUI>();
        invenUI.InitializeInventory(inven);

        inven.AddItem(ItemIDCode.HealingPotion);
        inven.AddItem(ItemIDCode.HealingPotion);
        inven.AddItem(ItemIDCode.HealingPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion);
        inven.AddItem(ItemIDCode.ManaPotion, 5);
        inven.AddItem(ItemIDCode.ManaPotion, 5);
        inven.AddItem(ItemIDCode.ManaPotion, 5);

        //inven.TempRemoveItem(1, 3);
        //inven.TempToSlot(6);
        //inven.ClearItem(0);
        //inven.RemoveItem(5);

    }

    private static void Test_AddRemoveMove()
    {
        Inventory inven = new();
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Bone);
        inven.AddItem(ItemIDCode.Bone);
        inven.AddItem(ItemIDCode.Bone);
        inven.AddItem(ItemIDCode.Bone);
        inven.RemoveItem(3);
        inven.RemoveItem(10);
        inven.PrintInventory();

        inven.AddItem(ItemIDCode.Egg, 3);
        inven.AddItem(ItemIDCode.Bone, 3);
        inven.PrintInventory();

        inven.ClearInventory();
        inven.PrintInventory();

        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Egg);
        inven.AddItem(ItemIDCode.Bone);
        inven.PrintInventory();

        inven.MoveItem(1, 4);
        inven.PrintInventory();
        inven.MoveItem(0, 1);
        inven.MoveItem(4, 7);
        inven.MoveItem(7, 0);
        inven.MoveItem(0, 7);
        inven.PrintInventory();

        inven.MoveItem(2, 3);
        inven.MoveItem(3, 3);
        inven.MoveItem(1, 3);
        inven.MoveItem(1, 1);
        inven.PrintInventory();

        inven.MoveItem(0, 1);
        inven.MoveItem(2, 1);
        inven.MoveItem(8, 0);
    }
}
