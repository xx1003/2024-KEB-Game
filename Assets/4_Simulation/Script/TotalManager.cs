using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TotalManager : MonoBehaviour
{
    public static TotalManager instance;
    public Image fadeScreen;
    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MoveScene(int id)
    {
        StartCoroutine(moveScenewithFade(id));
    }

    public void MoveScene(string sceneName)
    {
        StartCoroutine(moveScenewithFade(sceneName));

    }

    private IEnumerator moveScenewithFade(int id)
    {
        yield return StartCoroutine(FadeScreen(true));  //yield 어쩌고는 기다리는 거라고 생각해라 뭐래
        SceneManager.LoadScene(id);
        yield return StartCoroutine(FadeScreen(false));
    }
    
    private IEnumerator moveScenewithFade(string sceneName)
    {
        yield return StartCoroutine(FadeScreen(true));  //yield 어쩌고는 기다리는 거라고 생각해라 뭐래
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(FadeScreen(false));
    }
    private IEnumerator FadeScreen(bool fadeOut)
    {
        var fadeTimer = 0f;
        const float FadeDuration = 1f;

        var initialValue = fadeOut ? 0f : 1f;
        var fadeDir = fadeOut ? 1f : -1f;
        
        while (fadeTimer < FadeDuration)
        {
            yield return null;  // update랑 똑같은 기능???ㅗㅗㅗ
            fadeTimer += Time.deltaTime;

            var color = fadeScreen.color;

            initialValue += fadeDir * Time.deltaTime;
            color.a = initialValue;

            fadeScreen.color = color;
        }
        
        
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
