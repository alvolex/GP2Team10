using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerProfile : MonoBehaviour
{
    //SAVED INFORMATION GOES HERE, WHAT DO WE ACTUALLY WANT TO SAVE IN THE GAME?
    
    //Reputation, Tips, How hectic the game is at the current state, upgrade state, current day(?)

    public PlayerStatsScriptableObject savedStats;
    
    public int reputation;
    public int tips;
}
