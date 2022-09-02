using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI loadingText;
    public string nextSceneName = "Seamless_Base";

    private AsyncOperation async;

    private WaitForSeconds waitSecond;
    private IEnumerator loadingTextCoroutine;
    private IEnumerator loadSceneCoroutine;

    private float loadRatio = 0.0f;
    private bool loadCompleted = false;

    private PlayerInputAction inputActions;

    private float sliderUpdateSpeed = 1.0f;


    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Press.performed += OnMousePress;
    }

    private void OnDisable()
    {
        inputActions.UI.Press.performed -= OnMousePress;
        inputActions.UI.Disable();
    }

    private void OnMousePress(InputAction.CallbackContext obj)
    {
        if (loadCompleted)
        {
            async.allowSceneActivation = true;
        }
    }

    private void Start()
    {
        waitSecond = new WaitForSeconds(0.2f);
        loadingTextCoroutine = LoadingTextProgress();
        StartCoroutine(loadingTextCoroutine);
        loadSceneCoroutine = LoadScene();
        StartCoroutine(loadSceneCoroutine);
    }

    private void Update()
    {
        if (slider.value < loadRatio)
        {
            slider.value += Time.deltaTime * sliderUpdateSpeed;
        }

        //if(slider.value >= 1.0f)
            //loadingText.text = "Loading Completed";
    }

    private IEnumerator LoadingTextProgress()
    {
        int point = 0;  // 0 ~ 5로 변경된 숫자
        while (true)
        {
            string text = "Loading";
            for (int i = 0; i < point; i++)
            {
                text += " .";
            }
            loadingText.text = text;    // 반복해서 Loading 글자 뒤에 .을 추가로 붙인다.

            yield return waitSecond;    // 정해진 시간동한 대기
            point++;                    // point값 증가
            point %= 6;                 // point값이 0 ~ 5가 되도록 변경
        }
    }

    private IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (loadRatio < 1.0f)
        {
            loadRatio = async.progress + 0.1f;
            //slider.value = loadRatio;
            yield return null;
        }

        loadCompleted = true;           // 로딩이 끝났다고 표시
        Debug.Log("Load Complete!");

        yield return new WaitForSeconds(1.0f);  // 1초 뒤에 점찍는 것도 멈추기
        StopCoroutine(loadingTextCoroutine);
        loadingText.text = "Loading Complete.\nPress Button";
    }
}
