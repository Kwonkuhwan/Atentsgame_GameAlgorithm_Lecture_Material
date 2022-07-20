using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    // 기본 데이터 -----------------------------------------------------------------------------
    private Inventory inven;
    private Player player;

    public GameObject slotPrefab;
    private Transform slotParent;

    private ItemSlotUI[] slotUIs;
    // ----------------------------------------------------------------------------------------

    // 돈 데이터 -------------------------------------------------------------------------------
    private TextMeshProUGUI goldText;
    // ----------------------------------------------------------------------------------------

    private void Awake()
    {
        slotParent = transform.Find("ItemSlots");
        goldText = transform.Find("Gold").Find("GoldText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        player = GameManager.Inst.MainPlayer;
        player.OnMoneyChange += RefreshMoney;
        RefreshMoney(player.Money);
    }

    public void InitializeInventory(Inventory newInven)
    {
        inven = newInven;
        if(Inventory.Default_Inventory_Size != newInven.SlotCount)
        {
            // 기존 슬롯 전부 삭제
            ItemSlotUI[] slots = GetComponentsInChildren<ItemSlotUI>();
            foreach(var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            // 새로 만들기
            slotUIs = new ItemSlotUI[inven.SlotCount];
            for (int i=0; i<inven.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);
                obj.name = $"{slotPrefab.name}_{i}";
            }
        }
    }

    private void RefreshMoney(int money)
    {
        goldText.text = money.ToString("N0");
    }
}
