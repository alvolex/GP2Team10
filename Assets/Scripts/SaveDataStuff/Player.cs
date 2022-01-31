using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using UnityEngine;
using Variables;

public class Player : MonoBehaviour
{
    public int tips = 5;

    public IntVariable tipsReference;
    [SerializeField] private ScriptableEventInt onTipsChanged;
    

    
    
    public int reputation;
    public int currentMovementspeedUpgrade;
    public int currentCustomerMovementspeedUpgrade;
    public int currentSeatingUpgrade;

    public void SavePlayer()
    {
        SaveSystem.SavePlayerStats(this);
        Debug.Log("Saved");

    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        tipsReference.SetValue(data.tips);
        Debug.Log("Loaded");

    }
    private void Update()
    {
        Debug.Log(tipsReference.Value);
        
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SavePlayer();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadPlayer();
        }
    }
}
