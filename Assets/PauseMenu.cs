using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
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
    
    [Header("Reputation: ")] 
    [SerializeField] private IntVariable reputation;
    [SerializeField] private ScriptableEventInt OnRepChangedEvent;
    
    [Header("Tips: ")] 
    [SerializeField] private IntVariable tips;
    [SerializeField] private ScriptableEventInt OnTipChangedEvent;
    

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (userInterface.activeSelf == true)
            {
                ResumeGame();
            }
            else
            {
                Time.timeScale = 0;
                userInterface.SetActive(true);
            }
            
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        userInterface.SetActive(false);
    }

    public void NewGame()
    {
        
        tips.SetValue(0);
        OnTipChangedEvent.Raise();
        
        reputation.SetValue(0);
        OnRepChangedEvent.Raise();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame()
    {
        tips.SetValue(0);
        OnTipChangedEvent.Raise();
        
        reputation.SetValue(0);
        OnRepChangedEvent.Raise();
        
        Time.timeScale = 1;
        //SceneManager.LoadScene(mainMenu.name); //Doesnt work after making a build for some reason
        SceneManager.LoadScene("MAIN_MENU");
    }
    
}
