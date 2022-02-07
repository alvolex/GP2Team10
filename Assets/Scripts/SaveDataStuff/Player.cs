using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ScriptableEvents;
using UnityEngine;
using Variables;

public class Player : MonoBehaviour
{
    
    /*public static bool loadGame = false;

    
    public IntVariable tipsReference;
    public IntVariable reputationReference;

    private UpgradeSystem upgradeSystem;
    
    [HideInInspector]public int currentMovementSpeedUpgrade;
    [HideInInspector]public int currentCustomerMovementSpeedUpgrade;
    [HideInInspector]public int currentSeatingUpgrade;
    [HideInInspector]public int currentStorageUpgrade;

    [HideInInspector]public int movementSpeedUpgradesAvailable;
    [HideInInspector]public int alienMovementSpeedUpgradesAvailable;
    [HideInInspector]public int extraCookingStationUpgradesAvailable;
    [HideInInspector]public int extraStorageSlot;
    [HideInInspector]public int tableUpgradesAvailable;
    private void Start()
    {
        upgradeSystem = FindObjectOfType<UpgradeSystem>();

        if (loadGame)
        {
            LoadPlayer();
        }
    }
    
    public void LoadGame()
    {
        loadGame = true;
    }
    public void NewGame()
    {
        loadGame = false;
    }

    void SaveUpgradesAndPoints()
    {
        currentMovementSpeedUpgrade = upgradeSystem.CurrentMovementSpeedUpgrade;
        currentCustomerMovementSpeedUpgrade = upgradeSystem.CurrentCustomerMovementSpeedUpgrade;
        currentSeatingUpgrade = upgradeSystem.CurrentSeatingUpgrade;
        currentStorageUpgrade = upgradeSystem.CurrentStorageUpgrade;

        movementSpeedUpgradesAvailable = upgradeSystem.MovementSpeedUpgradesAvailable;
        alienMovementSpeedUpgradesAvailable = upgradeSystem.AlienMovementSpeedUpgradesAvailable;
        extraCookingStationUpgradesAvailable = upgradeSystem.ExtraCookingStationUpgradesAvailable;
        extraStorageSlot = upgradeSystem.ExtraCookingStationUpgradesAvailable;
        tableUpgradesAvailable = upgradeSystem.TableUpgradesAvailable;


    }
    void ApplyUpgrades(PlayerData playerData)
    {
        upgradeSystem.CurrentMovementSpeedUpgrade = playerData.currentMovementSpeedUpgrade;
        upgradeSystem.CurrentCustomerMovementSpeedUpgrade = playerData.currentCustomerMovementSpeedUpgrade;
        upgradeSystem.CurrentSeatingUpgrade = playerData.currentSeatingUpgrade;
        upgradeSystem.CurrentStorageUpgrade = playerData.currentStorageUpgrade;

        upgradeSystem.MovementSpeedUpgradesAvailable = playerData.movementSpeedUpgradesAvailable;
        upgradeSystem.AlienMovementSpeedUpgradesAvailable = playerData.alienMovementSpeedUpgradesAvailable;
        upgradeSystem.ExtraCookingStationUpgradesAvailable = playerData.extraCookingStationUpgradesAvailable;
        upgradeSystem.TableUpgradesAvailable = playerData.tableUpgradesAvailable;
        upgradeSystem.ExtraStorageSlot = playerData.extraStorageSlot;
        
        upgradeSystem.ApplyUpgrades();
    }
    
    
    public void SavePlayer()
    {
        SaveUpgradesAndPoints();
        SaveSystem.SavePlayerStats(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        tipsReference.SetValue(data.tips);
        reputationReference.SetValue(data.reputation);
        ApplyUpgrades(data);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SavePlayer();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadPlayer();
        }
    }*/
}
