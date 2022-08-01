using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailInfoUI : MonoBehaviour
{

    // 사용하는 컴포넌트들 ----------------------------------------------------------------------
    private CanvasGroup canvasGroup;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemPrice;
    private Image itemIcon;
    // ----------------------------------------------------------------------------------------

    // 기본 데이터 -----------------------------------------------------------------------------
    private ItemData itemData;      // 표시할 상세정보

    public bool IsPause = false;    // 상태 정보창 열고 닫기 일시 정지(true면 열리지 않고, false면 열린다.)
    // ----------------------------------------------------------------------------------------

    // 유닡티 이벤트 함수 ---------------------------------------------------------------------------------
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        itemPrice = transform.Find("Value").GetComponent<TextMeshProUGUI>();
        itemIcon = transform.Find("Icon").GetComponent<Image>();
    }
    // ----------------------------------------------------------------------------------------

    // 함수들 ---------------------------------------------------------------------------------
    /// <summary>
    /// 상세정보창 열기
    /// </summary>
    /// <param name="data">표시할 데이터</param>
    public void Open(ItemData data)
    {
        if (!IsPause)               // IsPause 상태가 아닐 때만 열기
        {
            itemData = data;        // 데이터 넣고
            Refresh();              // 화면 갱신
            canvasGroup.alpha = 1;  // 알파값 조절로 on/off 결정
        }
    }

    /// <summary>
    /// 상세정보창 닫기
    /// </summary>
    public void Close()
    {
        if (!IsPause)               // IsPause 상태가 아닐 때만 닫기
        {
            itemData = null;        // 데이터 비우기
            canvasGroup.alpha = 0;  // 알파값 조절해서 보이지 않게 만들기
        }
    }

    /// <summary>
    /// 가지고 있는 데이터 기반으로 화면 갱신
    /// </summary>
    private void Refresh()
    {
        if (itemData != null)   // 데이터가 있으면 데이터 갱신
        {
            itemName.text = itemData.itemName;
            itemPrice.text = itemData.value.ToString() + $"G";
            itemIcon.sprite = itemData.itemIcon;
        }
    }
    // ----------------------------------------------------------------------------------------
}
