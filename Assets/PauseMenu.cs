using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PauseMenu : MonoBehaviour
{
    private Scene scene;
    public Object mainMenu;
    public GameObject userInterface;
    private float time;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            Time.timeScale = 0;
            userInterface.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        userInterface.SetActive(false);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenu.name);
    }
    
}
