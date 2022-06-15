using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text score_Text = null;
    float currentScore = 0.0f;          // 보여질 스코어

    private void init()
    {
        GameManager.Inst.OnScoreChange = Refresh;
    }

    private void Awake()
    {
        score_Text = GetComponent<Text>();
    }

    private void Start()
    {
        init();
    }

    private void Update()
    {
        if (currentScore < GameManager.Inst.Score)      // 게임 매니저가 가지고 있는 실제 스코어 보다 작으면 계속 증가
        {
            currentScore += (Time.deltaTime * 10.0f);      // 1초에 최대 10까지 증가
            score_Text.text = $"Score : {(int)currentScore,4}";     // 무조건 score를 4자리로 표현
        }
    }

    void Refresh()
    {
        score_Text.text = $"Score : {(int)currentScore,4}";     // 무조건 score를 4자리로 표현
    }
}
