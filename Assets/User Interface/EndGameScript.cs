using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    private bool loading = false;
    private void Update()
    {
        if (!loading && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadYourAsyncScene());
        }
    }
    
    IEnumerator LoadYourAsyncScene()
    {
        loading = true;
        var asyncLoad = SceneManager.LoadSceneAsync("MainMenuScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
