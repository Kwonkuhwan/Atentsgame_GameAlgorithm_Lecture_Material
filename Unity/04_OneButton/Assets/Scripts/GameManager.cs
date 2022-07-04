using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    //private TextMeshProUGUI scoreText = null;
    private ImageNumber imageNumber = null;
    private ScoreBoard scoreBoard = null;
    private HighScoreBoard highScoreBoard = null;

    private float currentScore = 0.0f;
    public int point = 10;

    public const int rankCount = 5;
    public const int INVALID_RANK = -1;

    private string[] highName = new string[rankCount];
    private int[] highScore = new int[rankCount];
    public string[] HighName {get => highName;}
    public int BestScore { get => highScore[0]; }
    public int[] HighScore { get => highScore; }


    private int score = 0;
    public int Score 
    { 
        get => score;
        set 
        { 
            score = value;
            //OnScoreChange?.Invoke();
            //Debug.Log($"Score : {score}"); 
        }
    }

    // static 맴버 변수 : 주소가 고정 => 이 클래스의 모든 인스턴스는 같은 값을 가진다.
    private static GameManager instance = null;
    public static GameManager Inst { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    private void Update()
    {
        if (currentScore < GameManager.Inst.Score)
        {
            currentScore += (Time.deltaTime * 50.0f);
            //scoreText.text = $"{(int)currentScore,4}";
            imageNumber.Number = (int)currentScore;

        }
    }

    private void Initialize()
    {
        //scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        Score = 0;
        currentScore = 0;
        //scoreText.text = $"{score,4}";

        imageNumber = GameObject.Find("MainScore_ImageNumber").GetComponent<ImageNumber>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        highScoreBoard = FindObjectOfType<HighScoreBoard>();
        LoadGameData();
    }

    public void SaveGameData()
    {
        SaveData saveData = new();                      // json 저장용 클래스 인스턴스화
        saveData.highScore = highScore;                 // json 저장용 클래스에 값을 넣기
        saveData.highName = highName;

        string json = JsonUtility.ToJson(saveData);     // json 저장용 클래스의 내용을 json포맷의 문자열로 변경

        string path = $"{Application.dataPath}/Save/";  // 저장할 폴더의 경로 구하기
        if(!Directory.Exists(path))                     // 경로가 존재하는지 확인
        {
            Directory.CreateDirectory(path);            // 경로에 해당하는 폴더가 없으면 만들기
        }

        string fullPath = $"{path}Save.json";           // 경로명과 파일명을 합쳐서 전체경로(fullPath) 만들기
        File.WriteAllText(fullPath, json);              // 전체경로대로 파일을 만들어서 json 문자열 내용 쓰기
    }

    public void LoadGameData()
    {
        string path = $"{Application.dataPath}/Save/";      // 저장된 폴더 이름 만들기
        string fullPath = $"{path}Save.json";               // 전체경로 만들기

        if(Directory.Exists(path) && File.Exists(fullPath)) // 경로와 파일 둘 다 존재하는지 확인
        {
            string json = File.ReadAllText(fullPath);       // 실제로 파일에 써있는 문자열 읽기
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);   // 특정 클래스 규격에 맞게 파싱하기
            highScore = saveData.highScore;                 // json 데이터를 불러온 클래스에서 값 가져오기
            highName = saveData.highName;
        }
    }

    public void OnGameOver()
    {
        bool isBestScore = false;
        int rank = INVALID_RANK;

        for (int i = 0; i < rankCount; i++)
        {
            if (highScore[i] < score)
            {
                isBestScore = (i == 0);
                for (int j = rankCount - 1; j > i; j--)
                {
                    highScore[j] = highScore[j - 1];
                    highName[j] = highName[j - 1];
                }
                highScore[i] = score;
                rank = i;
                break;
            }            
        }
        scoreBoard.Open(isBestScore);
        highScoreBoard.Open(rank);
    }

    public void SetHighName(int rank, string newName)
    {
        highName[rank] = newName;
        SaveGameData();
    }
}
