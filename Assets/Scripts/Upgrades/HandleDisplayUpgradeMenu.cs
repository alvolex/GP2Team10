using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;

public class HandleDisplayUpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeUI;
    
    [Header("Events")]
    [SerializeField] private ScriptableSimpleEvent dayEnd;
    [SerializeField] private ScriptableSimpleEvent startNextDay;

    private void Start()
    {
        dayEnd.ScriptableEvent += ToggleUI;
    }

    void ToggleUI()
    {
        upgradeUI.SetActive(!upgradeUI.activeSelf);
        
        Time.timeScale = 0;
    }

    public void StartNextDay()
    {
        startNextDay.InvokeEvent();
        
        ToggleUI();
        Time.timeScale = 1;
    }
    
}
