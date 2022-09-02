using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Test_LoadingScene : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI loadingText;
    public string nextSceneName = "Test_Seamless_Base";

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
        if(loadCompleted)
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
        //slider.value = Mathf.Lerp(slider.value, loadRatio, Time.deltaTime * sliderUpdateSpeed);
    }

    private IEnumerator LoadingTextProgress()
    {
        int point = 0;

        while(true)
        {
            string text = "Loading";
            for (int i = 0; i < point; i++)
            {
                text += " .";
            }
            loadingText.text = text;

            yield return waitSecond;
            point++;
            point %= 6;
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

        loadCompleted = true;
        StopCoroutine(loadingTextCoroutine);
    }
}
