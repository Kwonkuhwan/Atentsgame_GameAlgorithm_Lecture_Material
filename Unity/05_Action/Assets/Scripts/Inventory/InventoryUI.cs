using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // 기본 데이터 -----------------------------------------------------------------------------
    private Inventory inven;
    private Player player;

    public GameObject slotPrefab;
    private Transform slotParent;

    private ItemSlotUI[] slotUIs;
    // ----------------------------------------------------------------------------------------

    // Item 데이터 -----------------------------------------------------------------------------
    uint dragStartID;
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
        if (Inventory.Default_Inventory_Size != newInven.SlotCount)
        {
            // 기존 슬롯 전부 삭제
            ItemSlotUI[] slots = GetComponentsInChildren<ItemSlotUI>();
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            // 새로 만들기
            slotUIs = new ItemSlotUI[inven.SlotCount];
            for (int i = 0; i < inven.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);
                obj.name = $"{slotPrefab.name}_{i}";
                slotUIs[i] = obj.GetComponent<ItemSlotUI>();
                slotUIs[i].Initialize((uint)i, inven[i]);
            }
        }
        else
        {
            slotUIs = GetComponentsInChildren<ItemSlotUI>();
            for(int i=0; i<inven.SlotCount; i++)
            {
                slotUIs[i].Initialize((uint)i, inven[i]);
            }
        }
        RefreshAllSlots();
    }

    private void RefreshAllSlots()
    {
        foreach(var slotUI in slotUIs)
        {
            slotUI.Refresh();
        }
    }

    private void RefreshMoney(int money)
    {
        goldText.text = money.ToString("N0");
    }

    // 이벤트 시스템의 인터페이스 함수들 ---------------------------------------------------------

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject startObj = eventData.pointerCurrentRaycast.gameObject;
            if(startObj != null)
            {
                ItemSlotUI slotUI = startObj.GetComponent<ItemSlotUI>();
                if(slotUI != null)
                {
                    Debug.Log($"Start SlotID : {slotUI.ID}");
                    dragStartID = slotUI.ID;
                }
            }
            //if(eventData.pointerCurrentRaycast.gameObject.transform.parent == null)
            //{ 
            //    Debug.Log("Is null");
            //}
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject endObj = eventData.pointerCurrentRaycast.gameObject;
            if (endObj != null)
            {
                ItemSlotUI slotUI = endObj.GetComponent<ItemSlotUI>();
                if (endObj != null)
                {
                    Debug.Log($"End SlotID : {slotUI.ID}");
                    inven.MoveItem(dragStartID, slotUI.ID);
                }
            }
        }
    }

    // ----------------------------------------------------------------------------------------
}
