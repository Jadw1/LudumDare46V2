using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;

    private bool isActive;
    
    public void CloseMenu()
    {
        if (!isActive) return;
        isActive = false;
        menu.SetActive(false);
    }

    public void OpenMenu()
    {
        if (isActive) return;
        isActive = true;
        menu.SetActive(true);
    }

    private void Update()
    {
        if (!isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
