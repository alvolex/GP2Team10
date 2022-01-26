using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerStats", menuName = "SO/ PlayerStats", order = 0)]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public int tips;
    public int reputation;
    public int currentMSUpgrade;
    public int currentCMSUpgrade;
    public int currentAllergenUpgrade;
    public int currentCookingStationUpgrade;
    public int currentStorageUpgrade;
    public int currentSeatingUpgrade;
    
    
    
    /*Getters*/


    public int Tips => tips;
    public int Reputation => reputation;
    public int CurrentMSUpgrade => currentMSUpgrade;
    public int CurrentCMSUpgrade => currentMSUpgrade;
    public int CurrentAllergenUpgrade => currentMSUpgrade;
    public int CurrentCookingStationUpgrade => currentMSUpgrade;
    public int CurrentStorageUpgrade => currentMSUpgrade;
    public int CurrentSeatingUpgrade => currentMSUpgrade;

}
