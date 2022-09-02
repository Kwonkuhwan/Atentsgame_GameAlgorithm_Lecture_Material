using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public Text PlayTimeText;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Show(false);
    }

    public void Show(bool isShow)
    {
        if(isShow)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
    public void GameOverLoading(float total)
    {
        Show(true);
        PlayTimeText.text = $"Total life time : {total:F2} sec";
    }

    public void OnReTryButtonClick()
    {
        SceneManager.LoadSceneAsync("LoadingScene",LoadSceneMode.Single);
    }
}
