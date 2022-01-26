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
    [SerializeField] public IntVariable reputation;
    [SerializeField] public TextMeshProUGUI reputationText;
    [SerializeField] public ScriptableEventInt OnRepChangedEvent;
    
    [Header("Tips: ")] 
    [SerializeField] public IntVariable tips;
    [SerializeField] public TextMeshProUGUI tipsText;
    [SerializeField] public ScriptableEventInt OnTipChangedEvent;
    
    [HideInInspector]public int currentMSUpgrade;
    [HideInInspector]public int currentCMSUpgrade;
    [HideInInspector]public int currentAllergenUpgrade;
    [HideInInspector]public int currentCookingStationUpgrade;
    [HideInInspector]public int currentStorageUpgrade;
    [HideInInspector]public int currentSeatingUpgrade;
    
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