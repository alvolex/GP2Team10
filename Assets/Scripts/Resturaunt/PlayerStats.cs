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
    }

    public void OnReputationChanged(int newValue)
    {
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
}