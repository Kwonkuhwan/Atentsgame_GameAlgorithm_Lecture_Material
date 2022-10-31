using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour
{
    readonly string url = "http://go2665.dothome.co.kr/HTTP_Data/TestData.txt";

    public const int rankCount = 5;
    private int[] highScore;
    private string[] rankerName;

    public int[] HighScore => highScore;
    public string[] RankerName => rankerName;

    void Start()
    {
        LoadGameData();
    }

    public void LoadGameData()
    {
        StartCoroutine(GetWebData());
    }

    IEnumerator GetWebData()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
        else
        {
            string json = www.downloadHandler.text;
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            highScore = saveData.highScore;
            rankerName = saveData.rankerName;

            for (int i = 0; i < highScore.Length; i++)
            {
                Debug.Log($"Ranker Name : {rankerName[i]}, High Score : {highScore[i]}");
            }
        }
    }
}