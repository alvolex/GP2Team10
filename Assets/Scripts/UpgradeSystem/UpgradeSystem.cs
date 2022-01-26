using System;
using UnityEngine;
using UnityEngine.UI;
using Variables;

public class UpgradeSystem : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject playerReference;
    [SerializeField] private IntReference aliensReference;
    [SerializeField] private IntReference allergensReference;
    [SerializeField] private IntReference tipsReference;
    [SerializeField] private Button upgradeButton;

    [Header("Movement Speed")] 
    [SerializeField] private int upgrade1GoalMS;
    [SerializeField] private int upgrade2GoalMS;
    [SerializeField] private int upgrade3GoalMS;
    
    [Header("Allergens Served limits")] 
    [SerializeField] private int allergensServedLimit1;
    [SerializeField] private int allergensServedLimit2;
    [SerializeField] private int allergensServedLimit3;

    [Header("Customer Movement Speed")] 
    [SerializeField] private int upgrade1GoalCMS;
    [SerializeField] private int upgrade2GoalCMS;
    [SerializeField] private int upgrade3GoalCMS;
    
    [Header("Upgrade Costs")]
    [SerializeField] private int[] movementSpeedUpgradeCost;
    [SerializeField] private int[] alienMovementSpeedUpgradeCost;
    [SerializeField] private int[] cookingStationUpgradeSlot;
    [SerializeField] private int[] storageSlotUpgradeCost;
    [SerializeField] private int[] extraSeatingUpgradeCost;
    
    [Header("Cooking Stations")]
    [SerializeField] private GameObject[] cookingStations;
    
    [Header("Tables")]
    [SerializeField] private GameObject[] tables;

    private int cookingStation;
    private int table;
    
    private int movementSpeedUpgradesAvailable;
    private int alienMovementSpeedUpgradesAvailable;
    private int extraCookingStationUpgradesAvailable;
    private int extraStorageSlot;
    private int extraSeatingTable;

    int currentMSUpgrade = 0;
    int currentCMSUpgrade = 0;
    int currentAllergenUpgrade = 0;
    int currentCookingStationUpgrade = 0;
    int currentStorageUpgrade = 0;
    int currentSeatingUpgrade = 0;
    
    

    private void Start()
    {
        upgradeButton.interactable = false;
    }
    public void CheckAliensFed()
    {
        if (aliensReference.GetValue() == upgrade1GoalMS)
            movementSpeedUpgradesAvailable++;
        if (aliensReference.GetValue() == upgrade2GoalMS)
            movementSpeedUpgradesAvailable++;
        if (aliensReference.GetValue() == upgrade3GoalMS)
            movementSpeedUpgradesAvailable++;
        CheckButton();
    }
    public void CustomersWithAllergiesServed()
    {
        if (allergensReference.GetValue() == allergensServedLimit1)
            alienMovementSpeedUpgradesAvailable++;
        if (allergensReference.GetValue() == allergensServedLimit2)
            alienMovementSpeedUpgradesAvailable++;
        if (allergensReference.GetValue() == allergensServedLimit3)
            alienMovementSpeedUpgradesAvailable++;
        CheckButton();
    }
    private void CheckButton()
    {
        if (movementSpeedUpgradesAvailable > 0)
            upgradeButton.interactable = true;

        if (alienMovementSpeedUpgradesAvailable > 0)
        {
            //Enable UpgradeButtonHere for 
        }
        if (extraCookingStationUpgradesAvailable > 0)
        {
            //Enable ExtraCookingStationUpgrades for 
        }
        if (extraStorageSlot > 0)
        {
            //Enable UpgradeButtonHere for 
        }
        if (extraSeatingTable > 0)
        {
            //Enable UpgradeButtonHere for 
        }

    }
    public void UpgradeMS()
    {
        if (tipsReference.GetValue() > movementSpeedUpgradeCost[currentMSUpgrade])
        {
            tipsReference.ApplyChange(-movementSpeedUpgradeCost[currentMSUpgrade]);
            Debug.Log("Upgraded something");
            //Adjust movement depenign on how to adjust it i guess
            movementSpeedUpgradesAvailable--;
            currentMSUpgrade++;
            if (movementSpeedUpgradesAvailable == 0)
            {
                upgradeButton.interactable = false;
            }
        }
    }
    public void UpgradeCustomerMS()
    {
        if (tipsReference.GetValue() > alienMovementSpeedUpgradeCost[currentCMSUpgrade])
        {
            tipsReference.ApplyChange(-alienMovementSpeedUpgradeCost[currentCMSUpgrade]);
            //Adjust movement depenign on how to adjust it i guess
            alienMovementSpeedUpgradesAvailable--;
            currentCMSUpgrade++;
            if (alienMovementSpeedUpgradesAvailable == 0)
            {
                upgradeButton.interactable = false;
            }
        }
    }
    public void UpgradeCookingStation()
    {
        if (tipsReference.GetValue() > cookingStationUpgradeSlot[currentCookingStationUpgrade])
        {
            tipsReference.ApplyChange(-cookingStationUpgradeSlot[currentCookingStationUpgrade]);
            //Adjust movement depenign on how to adjust it i guess
            
            cookingStations[cookingStation].SetActive(false);
            cookingStation++;
            cookingStations[cookingStation].SetActive(true);
            
            alienMovementSpeedUpgradesAvailable--;
            currentCMSUpgrade++;
            if (alienMovementSpeedUpgradesAvailable == 0)
            {
                upgradeButton.interactable = false;
            }
        }
    }
    public void UpgradeTable()
    {
        tables[table].SetActive(false);
        table++;
        tables[table].SetActive(true);
        
        if (tipsReference.GetValue() > extraSeatingUpgradeCost[currentSeatingUpgrade])
        {
            tipsReference.ApplyChange(-extraSeatingUpgradeCost[currentSeatingUpgrade]);
            //Adjust movement depenign on how to adjust it i guess
            
            //SPAWN TABLES IN DESIGNATED WAY
            
            alienMovementSpeedUpgradesAvailable--;
            currentCMSUpgrade++;
            if (alienMovementSpeedUpgradesAvailable == 0)
            {
                upgradeButton.interactable = false;
            }
        }
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            tipsReference.ApplyChange(+400);
        }
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            movementSpeedUpgradesAvailable++;
        }
        Debug.Log(tipsReference.GetValue());
    }
}