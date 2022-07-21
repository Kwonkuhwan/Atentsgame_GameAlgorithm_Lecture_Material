using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    // 아이템 이미지
    // 아이템 슬롯(아이템 데이터(아이템 이미지))

    private uint id;
    public uint ID { get => id; }

    private ItemSlot itemSlot;

    private Image itemImage;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void Initialize(uint newID, ItemSlot targetSlot)
    {
        id = newID;
        itemSlot = targetSlot;
        itemSlot.onSlotItemChage = Refresh;
    }

    public void Refresh()
    {
        if(itemSlot.SlotItemData != null)
        {
            itemImage.sprite = itemSlot.SlotItemData.itemIcon;
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
        }
    }
}
