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


    private void Start()
    {
        SetReputation($"Current Reputation: {reputation.Value}");
    }

    public void OnReputationChanged(int newValue)
    {
        
        SetReputation($"Current Reputation: {reputation.Value}");
    }


    private void SetReputation(string text)
    {
        reputationText.text = text;
    }
}