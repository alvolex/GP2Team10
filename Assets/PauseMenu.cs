using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Update is called once per frame
    
    private Scene scene;
    public TextMeshProUGUI tmp;

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

        tmp.text = $"{time += Time.deltaTime}";
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        userInterface.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(scene.name);
    }
    
}
