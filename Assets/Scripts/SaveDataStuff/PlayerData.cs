using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int tips;
    public int reputation;
    public int currentMSUpgrade;
    public int currentCMSUpgrade;
    public int currentAllergenUpgrade;
    public int currentCookingStationUpgrade;
    public int currentStorageUpgrade;
    public int currentSeatingUpgrade;

    public PlayerData(PlayerStats player)
    {
        tips = player.tips.Value;
        reputation = player.reputation.Value;
        
        currentMSUpgrade = player.currentMSUpgrade;
        currentCMSUpgrade = player.currentCMSUpgrade;
        currentAllergenUpgrade = player.currentAllergenUpgrade;
        currentCookingStationUpgrade = player.currentCookingStationUpgrade;
        currentStorageUpgrade = player.currentStorageUpgrade;
        currentSeatingUpgrade = player.currentSeatingUpgrade;
    }
    
}
