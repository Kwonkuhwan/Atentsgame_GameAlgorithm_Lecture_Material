using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Text life_Text = null;

    private void Awake()
    {
        life_Text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        GameManager.Inst.MainPlayer.OnHit += Refresh;
    }

    void Refresh()
    {
        life_Text.text = $"X {(int)GameManager.Inst.MainPlayer.Life}";     // 무조건 score를 4자리로 표현
    }
}
