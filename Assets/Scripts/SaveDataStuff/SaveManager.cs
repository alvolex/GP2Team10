using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private UpgradeSystem playerUpgrades;
    private PlayerData playerData;
    
    
    private void Start()
    {
        playerUpgrades = FindObjectOfType<UpgradeSystem>();
    }
    
}
