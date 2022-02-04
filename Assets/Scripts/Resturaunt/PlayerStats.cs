using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Variables;

public class PlayerStats : MonoBehaviour
{
    [Header("Loosing Condition Reputation: ")] [SerializeField]
    private int loosingCondition;

    [SerializeField] private Button restartGameButton;
    

    
    
    [Header("Reputation: ")] 
    [SerializeField] private IntVariable reputation;
    [SerializeField] private TextMeshProUGUI reputationText;
    [SerializeField] private ScriptableEventInt OnRepChangedEvent;
    
    [Header("Tips: ")] 
    [SerializeField] private IntVariable tips;
    [SerializeField] private TextMeshProUGUI tipsText;
    [SerializeField] private ScriptableEventInt OnTipChangedEvent;


    private void Start()
    {
        SetReputation($"{reputation.Value}");
        SetTip($"{tips.Value}");
        restartGameButton.gameObject.SetActive(false);
    }

    public void OnReputationChanged(int newValue)
    {
        if (reputation.Value <= loosingCondition)
        {
            
            restartGameButton.gameObject.SetActive(true);
            Debug.Log("You lose lol");
            Time.timeScale = 0;
            
            
        }
        
        SetReputation($"{reputation.Value}");
    }
    public void OnTipsChanged(int newValue)
    {
        SetTip($"{tips.Value}");
    }

    private void SetTip(string text)
    {
        tipsText.text = text;
    }
    private void SetReputation(string text)
    {
        reputationText.text = text;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }
    
    
}