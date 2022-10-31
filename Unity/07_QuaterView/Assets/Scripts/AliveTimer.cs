using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AliveTimer : MonoBehaviour
{
    private TextMeshProUGUI timer;

    private float time = 0.0f;
    private bool isTimerWork = false;

    private void Awake()
    {
        timer = transform.GetChild(2).GetComponent<TextMeshProUGUI>();   
    }

    private void Start()
    {
        PlayerTank player = FindObjectOfType<PlayerTank>();
        player.onDead += TimerStop;

        TimerReset();
    }

    private void Update()
    {
        if (isTimerWork)
        {
            time += Time.deltaTime;
            timer.text = $"{time:f2}";
        }
    }

    private void TimerReset()
    {
        time = 0.0f;
        isTimerWork = true;
    }

    private void TimerStop()
    {
        isTimerWork = false;
    }
}
