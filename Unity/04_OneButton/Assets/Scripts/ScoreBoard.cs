using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    public int[] Score = null;
    public Sprite[] madalSprites = null;

    private ImageNumber score = null;
    private ImageNumber highScore = null;
    private Image highScoreMark = null;
    private Image madalImage = null;

    private void Awake()
    {
        score = transform.Find("Score_ImageNumber").GetComponent<ImageNumber>();
        highScore = transform.Find("HighScore_ImageNumber").GetComponent<ImageNumber>();
        highScoreMark = transform.Find("New_Image").GetComponent<Image>();
        madalImage = transform.Find("Madal_Image").GetComponent<Image>();

        Button startButton = transform.Find("Start_Button").GetComponent<Button>();
        startButton.onClick.AddListener(OnReStartButtonClick);
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        score.Number = GameManager.Inst.Score;
        highScore.Number = GameManager.Inst.BestScore;
    }

    public void Open(bool isHighScore)
    {
        if(isHighScore)
        {
            highScoreMark.enabled = true;
        }

        if(GameManager.Inst.Score >= Score[3])
        {
            madalImage.sprite = madalSprites[3];
            madalImage.color = Color.white;
        }
        else if(GameManager.Inst.Score >= Score[2])
        {
            madalImage.sprite = madalSprites[2];
            madalImage.color = Color.white;
        }
        else if (GameManager.Inst.Score >= Score[1])
        { 
            madalImage.sprite = madalSprites[1];
            madalImage.color = Color.white;
        }
        else if (GameManager.Inst.Score >= Score[0])
        { 
            madalImage.sprite = madalSprites[0];
            madalImage.color = Color.white;
        }
        else
        {
            madalImage.color = Color.clear;
        }

        this.gameObject.SetActive(true);
    }

    private void OnReStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
