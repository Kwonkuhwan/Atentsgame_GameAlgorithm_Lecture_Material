using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreBoard : MonoBehaviour
{
    const int NumberOfHighScore = 5;
    HighScoreLine[] highScoreLines = null;
    TMP_InputField inputName;
    int inputRank = GameManager.INVALID_RANK;

    private void Awake()
    {
        //highScoreLines = FindObjectsOfType<HighScoreLine>();

        highScoreLines = new HighScoreLine[NumberOfHighScore];
        for (int i=0; i<transform.childCount-1; i++)
        {
            highScoreLines[i] = transform.GetChild(i).GetComponent<HighScoreLine>();   
        }
        inputName = transform.GetChild(transform.childCount - 1).GetComponent<TMP_InputField>();
        inputName.onEndEdit.AddListener(OnInputNameEnd);
        inputName.gameObject.SetActive(false);
    }

    private void OnInputNameEnd(string input)
    {
        highScoreLines[inputRank].SetHighName(input);
        inputName.gameObject.SetActive(false);
        GameManager.Inst.SetHighName(inputRank, input);
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Open(int rank)
    {
        this.gameObject.SetActive(true);

        for(int i = 0; i < NumberOfHighScore; i++)
        {
            highScoreLines[i].SetHighName(GameManager.Inst.HighName[i]);
            highScoreLines[i].SetHighScore(GameManager.Inst.HighScore[i]);
        }

        if(rank != GameManager.INVALID_RANK)
        {
            RectTransform rect = (RectTransform)inputName.transform;
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -52 + (-126 * rank));

            inputRank = rank;
            inputName.gameObject.SetActive(true);
        }
    }
}
