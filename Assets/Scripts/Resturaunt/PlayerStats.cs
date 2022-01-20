using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using TMPro;
using UnityEngine;
using Variables;

public class PlayerStats : MonoBehaviour
{
    [Header("Reputation: ")] 
    [SerializeField] private IntVariable reputation;
    [SerializeField] private IntReference reputationReference;
    [SerializeField] private TextMeshProUGUI reputationText;
    [SerializeField] private ScriptableEventInt OnRepChangedEvent;
    
    [Header("Tips: ")] 
    [SerializeField] private IntVariable tips;
    [SerializeField] private IntReference tipsReference;
    [SerializeField] private TextMeshProUGUI tipsText;
    [SerializeField] private ScriptableEventInt OnTipChangedEvent;


    private void Start()
    {
        SetReputation($"Current Reputation: {reputation.Value}");
        SetTip($"Current Reputation: {tips.Value}");
    }

    public void OnReputationChanged(int newValue)
    {
        
        SetReputation($"Current Reputation: {reputation.Value}");
    }
    public void OnTipsChanged(int newValue)
    {
        
        SetTip($"Current Tips: {tips.Value}");
    }

    private void SetTip(string text)
    {
        tipsText.text = text;
    }


    private void SetReputation(string text)
    {
        reputationText.text = text;
    }
}