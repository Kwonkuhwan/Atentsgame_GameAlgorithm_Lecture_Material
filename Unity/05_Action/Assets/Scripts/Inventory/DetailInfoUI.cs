using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailInfoUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemPrice;
    private Image itemIcon;

    private ItemData itemData;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        itemPrice = transform.Find("Value").GetComponent<TextMeshProUGUI>();
        itemIcon = transform.Find("Icon").GetComponent<Image>();
    }

    public void Open(ItemData data)
    {
        itemData = data;
        Refresh();
        canvasGroup.alpha = 1;
    }

    public void Close()
    {
        itemData = null;
        canvasGroup.alpha = 0;
    }

    private void Refresh()
    {
        itemName.text = itemData.itemName;
        itemPrice.text = itemData.value.ToString() + $"G";
        itemIcon.sprite = itemData.itemIcon;
    }
}
