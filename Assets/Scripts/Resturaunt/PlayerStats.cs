using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Variables;
using Object = UnityEngine.Object;

public class PlayerStats : MonoBehaviour
{
    
    
    [Header("Start Reputation and Money")] 
    [SerializeField] private int startingMoney;
    [SerializeField] private int startingRep;

    
    
    [Header("Loosing Condition Reputation: ")] [SerializeField]
    private int loosingCondition;


    [Header("Buttons")]
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button quitToMenuButton;
    private PlayerStateHandler currentState;


    public Object mainMnueSCENE;
    
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
        tips.SetValue(startingMoney);
        OnTipChangedEvent.Raise();
        
        reputation.SetValue(startingRep);
        OnRepChangedEvent.Raise();
        
        SetReputation($"{reputation.Value}");
        SetTip($"{tips.Value}");
        currentState = GetComponent<PlayerStateHandler>();
        
        restartGameButton.gameObject.SetActive(false);
        quitToMenuButton.gameObject.SetActive(false);
    }

    public void OnReputationChanged(int newValue)
    {
        if (reputation.Value <= loosingCondition)
        {
            YouLost();
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

    public void YouLost()
    {
        
        restartGameButton.gameObject.SetActive(true);
        quitToMenuButton.gameObject.SetActive(true);
        
        
        tips.SetValue(0);
        OnTipChangedEvent.Raise();
        
        reputation.SetValue(0);
        OnRepChangedEvent.Raise();
        
        Debug.Log("You lose lol");
        Time.timeScale = 0;
    }
    
    

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(mainMnueSCENE.name);
    }
    
    
    
}