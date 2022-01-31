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
    public int currentMovementspeedUpgrade;
    public int currentCustomerMovementspeedUpgrade;
    public int currentSeatingUpgrade;

    public PlayerData(Player player)
    {
        tips = player.tipsReference.Value;
        //currentMovementspeedUpgrade = upgradeSystem.CurrentMovementSpeedUpgrade;
       // currentCustomerMovementspeedUpgrade = upgradeSystem.CurrentCustomerMovementSpeedUpgrade;
        //currentSeatingUpgrade = upgradeSystem.CurrentSeatingUpgrade;
    }
}
