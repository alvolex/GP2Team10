using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    //SAVED INFORMATION GOES HERE, WHAT DO WE ACTUALLY WANT TO SAVE IN THE GAME?
    //Reputation, Tips, How hectic the game is at the current state, upgrade state, current day(?)
    public int level;
    
    
    public int reputation;
    public int tips;
    public int currentMovementSpeedUpgrade;
    public int currentCustomerMovementSpeedUpgrade;
    public int currentSeatingUpgrade;
    public int currentStorageUpgrade;
    
    public int movementSpeedUpgradesAvailable;
    public int alienMovementSpeedUpgradesAvailable;
    public int extraCookingStationUpgradesAvailable;
    public int extraStorageSlot;
    public int tableUpgradesAvailable;

    public PlayerData(Player player)
    {
        tips = player.tipsReference.Value;
        reputation = player.reputationReference.Value;
        
        currentMovementSpeedUpgrade = player.currentMovementSpeedUpgrade;
        currentCustomerMovementSpeedUpgrade = player.currentCustomerMovementSpeedUpgrade;
        currentSeatingUpgrade = player.currentSeatingUpgrade;
        currentStorageUpgrade = player.currentStorageUpgrade;
        
        movementSpeedUpgradesAvailable = player.movementSpeedUpgradesAvailable;
        alienMovementSpeedUpgradesAvailable = player.alienMovementSpeedUpgradesAvailable;
        extraCookingStationUpgradesAvailable = player.extraCookingStationUpgradesAvailable;
        extraStorageSlot = player.extraStorageSlot;
        tableUpgradesAvailable = player.tableUpgradesAvailable;
        
    }
}
