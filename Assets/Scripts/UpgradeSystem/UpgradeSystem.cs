using System;
using ScriptableEvents;
using Scriptables;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Variables;

public class UpgradeSystem : MonoBehaviour
{
    [Header("References")] 
    
    
    [SerializeField] private GameObject playerReference;
    [SerializeField] private GameObject customerReference;
    
    
    [SerializeField] private IntReference aliensReference;
    [SerializeField] private IntReference allergensReference;
    [SerializeField] private IntReference tipsReference;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button MovementSpeedUpgradeButton;
    [SerializeField] private Button CustomerMovementSpeedUpgradeButton;
    [SerializeField] private Button TableUpgradeButton;
    [SerializeField] private Button CookingStationUpgradeButton;
    [SerializeField] private Button StorageUpgradeButton;

    
    
    
    [Header("Movement Speed")] 
    [SerializeField] private int movementSpeedUpgradeAmount;
    [SerializeField] private int AlienMovementSpeedUpgradeAmount;

    
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
    private int tableUpgradesAvailable;

    int currentMSUpgrade = 0;
    int currentCMSUpgrade = 0;
    //int currentAllergenUpgrade = 0;
    int currentCookingStationUpgrade = 0;
    //int currentStorageUpgrade = 0;
    int currentSeatingUpgrade = 0;
    
    [SerializeField] private ScriptableSimpleEvent dayEnd;
    [SerializeField] private ScriptableEventIntReference onAlienFed;
    [SerializeField] private ScriptableEventOneValue customerMovementSpeedChange;



    private void Start()
    {
        dayEnd.ScriptableEvent += CheckMoney;
    }

    public void CheckMoney()
    {
        if (currentMSUpgrade+1>movementSpeedUpgradeCost.Length || 
            tipsReference.GetValue() < movementSpeedUpgradeCost[currentMSUpgrade] ||
            movementSpeedUpgradesAvailable==0)
        {
            MovementSpeedUpgradeButton.interactable = false;
        }
        if (currentCMSUpgrade+1>alienMovementSpeedUpgradeCost.Length ||
            tipsReference.GetValue()<alienMovementSpeedUpgradeCost[currentCMSUpgrade] ||
            alienMovementSpeedUpgradesAvailable==0)
        {
            CustomerMovementSpeedUpgradeButton.interactable = false;
        }
        if (currentSeatingUpgrade+1>=extraSeatingUpgradeCost.Length || 
            tipsReference.GetValue() < extraSeatingUpgradeCost[currentSeatingUpgrade])
        {
            TableUpgradeButton.interactable = false;
        }
        
        CookingStationUpgradeButton.interactable = false;
        
    }
    public void CheckAliensFed()
    {
        if (aliensReference.GetValue() == upgrade1GoalMS)
            movementSpeedUpgradesAvailable++;
        if (aliensReference.GetValue() == upgrade2GoalMS)
            movementSpeedUpgradesAvailable++;
        if (aliensReference.GetValue() == upgrade3GoalMS)
            movementSpeedUpgradesAvailable++;
        
    }
    public void CustomersWithAllergiesServed()
    {
        Debug.Log("Gave allergic food");
        
        if (allergensReference.GetValue() == allergensServedLimit1)
            alienMovementSpeedUpgradesAvailable++;
        if (allergensReference.GetValue() == allergensServedLimit2)
            alienMovementSpeedUpgradesAvailable++;
        if (allergensReference.GetValue() == allergensServedLimit3)
            alienMovementSpeedUpgradesAvailable++;
        
    }
    public void UpgradeMS()
    {
        
        if (tipsReference.GetValue() > movementSpeedUpgradeCost[currentMSUpgrade])
        {
            tipsReference.ApplyChange(-movementSpeedUpgradeCost[currentMSUpgrade]);
            Debug.Log("Upgraded something");
            //Adjust movement depending on how to adjust it i guess
            playerReference.GetComponent<PlayerMovement>().MovementSpeed += movementSpeedUpgradeAmount;
            currentMSUpgrade++;
            movementSpeedUpgradesAvailable--;

            CheckMoney();
        }
    }
    public void UpgradeCustomerMS()
    {
        if (tipsReference.GetValue() > alienMovementSpeedUpgradeCost[currentCMSUpgrade])
        {
            tipsReference.ApplyChange(-alienMovementSpeedUpgradeCost[currentCMSUpgrade]);
            customerMovementSpeedChange.InvokeEvent(AlienMovementSpeedUpgradeAmount);
            currentCMSUpgrade++;
            alienMovementSpeedUpgradesAvailable--;
            
            CheckMoney();
        }
    }
    public void UpgradeCookingStation()
    {
        if (tipsReference.GetValue() > cookingStationUpgradeSlot[currentCookingStationUpgrade])
        {
            tipsReference.ApplyChange(-cookingStationUpgradeSlot[currentCookingStationUpgrade]);
            //Adjust movement depending on how to adjust it i guess
            
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
        tipsReference.ApplyChange(-extraSeatingUpgradeCost[currentSeatingUpgrade]);
        tables[currentSeatingUpgrade].GetComponent<Table>().UnlockTable();
        currentSeatingUpgrade++;
        tableUpgradesAvailable--;
        
        CheckMoney();
            
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            tipsReference.ApplyChange(+500);
            aliensReference.ApplyChange(+1);
            onAlienFed.Raise(aliensReference.GetValue());
        }

        //Debug.Log(tipsReference.GetValue());
        
    }
}