using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoolTimeSlot : MonoBehaviour
{
    protected Image progressImage;
    protected TextMeshProUGUI coolTimeText;
    private GameObject selected;

    private void Awake()
    {
        progressImage = transform.GetChild(1).GetComponent<Image>();
        coolTimeText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        selected = transform.GetChild(3).gameObject;
    }

    public virtual void RefreshUI(float current, float max)
    {
        if(current < 0) current = 0;
        coolTimeText.text = $"{current:f1}";
        progressImage.fillAmount = current / max;
    }

    public void SetSelected(bool show)
    {
        selected.SetActive(show);
    }

    public void SetDurationmode(bool mode)
    {
        if (mode)
        {
            progressImage.color = Color.green;
        }
        else
        {
            progressImage.color = Color.red;
        }
    }
}
