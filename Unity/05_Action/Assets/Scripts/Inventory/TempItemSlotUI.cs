using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class TempItemSlotUI : ItemSlotUI
{
    private PointerEventData eventData;

    /// <summary>
    /// Awake을 override해서 부모의 Awake 실행안되게 만들기
    /// </summary>
    protected override void Awake()
    {
        itemImage = GetComponent<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        eventData = new PointerEventData(EventSystem.current);
    }

    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    /// <summary>
    /// 임시 슬롯을 보이도록 열기
    /// </summary>
    /// <param name="slot">임시 슬롯에 할당할 아이템이 들어있는 슬롯</param>
    public void Open()
    {
        if (!ItemSlot.IsEmpty())        // 슬롯에 아이템이 들어있을 때만 열기
        {
            transform.position = Mouse.current.position.ReadValue();
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 임시 슬롯이 보이지 않게 닫기
    /// </summary>
    public void Close()
    {
        itemSlot.ClearSlotItem();
        gameObject.SetActive(false);
    }

    public bool IsEmpty() => itemSlot.IsEmpty();


    public void OnDrop(InputAction.CallbackContext context)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        eventData.position = mousePos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        if (results.Count < 1 && !IsEmpty())
        {
            Debug.Log("UI 바깥쪽");

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Ground")))
            {
                Vector3 pos = GameManager.Inst.MainPlayer.ItemDropPosition(hit.point);
                ItemFactory.MakeItem(ItemSlot.SlotItemData.id, pos, ItemSlot.ItemCount);

                Close();
            }
        }
    }
}
