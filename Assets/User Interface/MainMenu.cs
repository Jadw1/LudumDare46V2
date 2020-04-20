using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Slider _slider;

    private void DisableSlider()
    {
        _slider.enabled = false;

        foreach (var image in _slider.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }
    }
    
    private void EnableSlider()
    {
        _slider.enabled = true;

        foreach (var image in _slider.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
        }
    }

    private void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        DisableSlider();
    }

    public void StartGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator LoadYourAsyncScene()
    {
        EnableSlider();
        
        var asyncLoad = SceneManager.LoadSceneAsync("GameScene");

        while (!asyncLoad.isDone)
        {
            _slider.value = asyncLoad.progress;
            yield return null;
        }
    }
}