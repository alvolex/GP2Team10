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
    [SerializeField] private IntReference aliensReference;
    [SerializeField] private IntReference allergensReference;
    [SerializeField] private IntReference tipsReference;
    [SerializeField] private IntReference RepReputation;
    [SerializeField] private IntReference startersReference;
    [SerializeField] private IntReference mainCourseReference;
    [SerializeField] private IntReference dessertReference;
    
    [Header("Event callers")]
    [SerializeField] private ScriptableEventIntReference onTipsChanged;
    [SerializeField] private ScriptableEventIntReference OnReputationChanged;
    
    [SerializeField] private Button MovementSpeedUpgradeButton;
    [SerializeField] private Button CustomerMovementSpeedUpgradeButton;
    [SerializeField] private Button TableUpgradeButton;
    [SerializeField] private Button CookingStationUpgradeButton;
    [SerializeField] private Button StorageUpgradeButton;
    
    [Header("Movement Speed")] 
    [SerializeField] private int movementSpeedUpgradeAmount;
    [SerializeField] private int AlienMovementSpeedUpgradeAmount;

    /*[Header("Customers fed goal")]
    [SerializeField] private int upgrade1GoalMS;
    [SerializeField] private int upgrade2GoalMS;
    [SerializeField] private int upgrade3GoalMS;
    
    
    /*[Header("Allergens Served limits")] 
    [SerializeField] private int allergensServedLimit1;
    [SerializeField] private int allergensServedLimit2;
    [SerializeField] private int allergensServedLimit3;
    [Header("Starters Goals")] 
    [SerializeField] private int startersFedGoal1;
    [SerializeField] private int startersFedGoal2;
    [SerializeField] private int startersFedGoal3;
    [Header("Main Course Goals")] 
    [SerializeField] private int mainCourseGoal1;
    [SerializeField] private int mainCourseGoal2;
    [SerializeField] private int mainCourseGoal3;
    [Header("Dessert Goals")] 
    [SerializeField] private int dessertGoal1;
    [SerializeField] private int dessertGoal2;
    [SerializeField] private int dessertGoal3;*/
    
    [Header("Upgrade Costs")]
    [SerializeField] private int[] movementSpeedUpgradeCost;
    [SerializeField] private int[] alienMovementSpeedUpgradeCost;
    [SerializeField] private int[] cookingStationUpgradeCost;
    [SerializeField] private int[] storageSlotUpgradeCost;
    [SerializeField] private int[] extraSeatingUpgradeCost;

    [Header("Tables")]
    [SerializeField] private GameObject[] tables;
    
    private int movementSpeedUpgradesAvailable;
    private int alienMovementSpeedUpgradesAvailable;
    private int extraCookingStationUpgradesAvailable;
    private int extraStorageUpgradesAvailable;
    private int tableUpgradesAvailable;

    int currentMSUpgrade = 0;
    int currentCMSUpgrade = 0;
    int currentCookingStationUpgrade = 0;
    int currentStorageUpgrade = 0;
    int currentSeatingUpgrade = 0;
    
    [SerializeField] private ScriptableSimpleEvent dayEnd;
    [SerializeField] private ScriptableEventIntReference onAlienFed;
    [SerializeField] private ScriptableEventOneValue customerMovementSpeedChange;

    [SerializeField] private FoodPickupStation foodPickupStation;
    [SerializeField] private Kitchen kitchen;
    
    public int CurrentMovementSpeedUpgrade
    {
        get => currentMSUpgrade;
        set => currentMSUpgrade = value;
    }
    public int CurrentCustomerMovementSpeedUpgrade
    {
        get => currentCMSUpgrade;
        set => currentCMSUpgrade = value;
    }
    public int CurrentSeatingUpgrade
    {
        get => currentSeatingUpgrade;
        set => currentSeatingUpgrade = value;
    }
    public int CurrentStorageUpgrade
    {
        get => currentStorageUpgrade;
        set => currentStorageUpgrade = value;
    }
    public int MovementSpeedUpgradesAvailable
    {
        get => movementSpeedUpgradesAvailable;
        set => movementSpeedUpgradesAvailable = value;
    }
    public int AlienMovementSpeedUpgradesAvailable
    {
        get => alienMovementSpeedUpgradesAvailable;
        set => alienMovementSpeedUpgradesAvailable = value;
    }
    public int ExtraCookingStationUpgradesAvailable
    {
        get => extraCookingStationUpgradesAvailable;
        set => extraCookingStationUpgradesAvailable = value;
    }
    public int ExtraStorageSlot
    {
        get => extraStorageUpgradesAvailable;
        set => extraStorageUpgradesAvailable = value;
    }
    public int TableUpgradesAvailable
    {
        get => tableUpgradesAvailable;
        set => tableUpgradesAvailable = value;
    }
    private void Start()
    {
        dayEnd.ScriptableEvent += CheckMoney;
        foodPickupStation = FindObjectOfType<FoodPickupStation>();
        kitchen = FindObjectOfType<Kitchen>();
        
        CheckMoney();

    }


    private void OnDestroy()
    {
        dayEnd.ScriptableEvent -= CheckMoney;
    }

    public void CheckMoney()
    {
        if (currentMSUpgrade+1>movementSpeedUpgradeCost.Length || 
            tipsReference.GetValue() < movementSpeedUpgradeCost[currentMSUpgrade])
        {
            MovementSpeedUpgradeButton.interactable = false;
            //MovementSpeedUpgradeButton.gameObject.SetActive(false);
        }
        else
        {
            //MovementSpeedUpgradeButton.gameObject.SetActive(true);
            MovementSpeedUpgradeButton.interactable = true;
        }
        if (currentCMSUpgrade+1>alienMovementSpeedUpgradeCost.Length ||
            tipsReference.GetValue()<alienMovementSpeedUpgradeCost[currentCMSUpgrade])
        {
            //CustomerMovementSpeedUpgradeButton.gameObject.SetActive(false);
            CustomerMovementSpeedUpgradeButton.interactable = false;
        }
        else
        {
            //CustomerMovementSpeedUpgradeButton.gameObject.SetActive(true);
            CustomerMovementSpeedUpgradeButton.interactable = true;;
            
        }
        if (currentSeatingUpgrade+1>tables.Length || 
            tipsReference.GetValue() < extraSeatingUpgradeCost[currentSeatingUpgrade])
        {
            //TableUpgradeButton.gameObject.SetActive(false);
            TableUpgradeButton.interactable = false;
        }
        else
        {
            //TableUpgradeButton.gameObject.SetActive(true);
            TableUpgradeButton.interactable = true;

        }
        if (currentStorageUpgrade>= 2 || 
            tipsReference.GetValue() < storageSlotUpgradeCost[currentStorageUpgrade])
        {
            //StorageUpgradeButton.gameObject.SetActive(false);
            StorageUpgradeButton.interactable = false;
        }
        else
        {
            //StorageUpgradeButton.gameObject.SetActive(true);
            StorageUpgradeButton.interactable = true;
        }
        if (currentCookingStationUpgrade+1 > cookingStationUpgradeCost.Length ||
            tipsReference.GetValue() < cookingStationUpgradeCost[currentCookingStationUpgrade])
        {
            //CookingStationUpgradeButton.gameObject.SetActive(false);
            CookingStationUpgradeButton.interactable = false;
        }
        else
        {
            //CookingStationUpgradeButton.gameObject.SetActive(true);
            CookingStationUpgradeButton.interactable = true;
        }
    }
    /*public void CheckAliensFed()
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
        if (allergensReference.GetValue() == allergensServedLimit1)
            alienMovementSpeedUpgradesAvailable++;
        if (allergensReference.GetValue() == allergensServedLimit2)
            alienMovementSpeedUpgradesAvailable++;
        if (allergensReference.GetValue() == allergensServedLimit3)
            alienMovementSpeedUpgradesAvailable++;
    }
    public void StarterFed()
    {
        if (startersReference.GetValue() == startersFedGoal1)
            extraCookingStationUpgradesAvailable++;
        if (startersReference.GetValue() == startersFedGoal2)
            extraCookingStationUpgradesAvailable++;
        if (startersReference.GetValue() == startersFedGoal3)
            extraCookingStationUpgradesAvailable++;
    }
    public void MainCourseFed()
    {
        if (mainCourseReference.GetValue() == mainCourseGoal1)
            extraStorageUpgradesAvailable++;
        if (mainCourseReference.GetValue() == mainCourseGoal2)
            extraStorageUpgradesAvailable++;
        if (mainCourseReference.GetValue() == mainCourseGoal3)
            extraStorageUpgradesAvailable++;
    }
    public void DessertFed()
    {
        Debug.Log("Desert fed");
        
        if (dessertReference.GetValue() == dessertGoal1)
            tableUpgradesAvailable++;
        if (dessertReference.GetValue() == dessertGoal2)
            tableUpgradesAvailable++;
        if (dessertReference.GetValue() == dessertGoal3)
            tableUpgradesAvailable++;
    }*/
    public void UpgradeMS()
    {
        if (tipsReference.GetValue() > movementSpeedUpgradeCost[currentMSUpgrade])
        {
            tipsReference.ApplyChange(-movementSpeedUpgradeCost[currentMSUpgrade]);
            onTipsChanged.Raise(tipsReference.GetValue());
            playerReference.GetComponent<PlayerMovement>().MovementSpeed += movementSpeedUpgradeAmount;
            currentMSUpgrade++;
            //movementSpeedUpgradesAvailable--;

            CheckMoney();
        }
    }
    public void UpgradeCustomerMS()
    {
        if (tipsReference.GetValue() > alienMovementSpeedUpgradeCost[currentCMSUpgrade])
        {
            tipsReference.ApplyChange(-alienMovementSpeedUpgradeCost[currentCMSUpgrade]);
            onTipsChanged.Raise(tipsReference.GetValue());
            
            customerMovementSpeedChange.InvokeEvent(AlienMovementSpeedUpgradeAmount);
            currentCMSUpgrade++;
            //alienMovementSpeedUpgradesAvailable--;
            
            CheckMoney();
        }
    }
    public void UpgradeTable()
    {
        tipsReference.ApplyChange(-extraSeatingUpgradeCost[currentSeatingUpgrade]);
        onTipsChanged.Raise(tipsReference.GetValue());
        tables[currentSeatingUpgrade].GetComponent<Table>().UnlockTable();
        currentSeatingUpgrade++;
        //tableUpgradesAvailable--;
        CheckMoney();
    }
    public void UpgradeStorage()
    {
        tipsReference.ApplyChange(-storageSlotUpgradeCost[currentStorageUpgrade]);
        onTipsChanged.Raise(tipsReference.GetValue());
        //extraStorageUpgradesAvailable--;
        currentStorageUpgrade++;
        
        CheckMoney();
    }
    public void UpgradeCookingStation()
    {
        tipsReference.ApplyChange(-cookingStationUpgradeCost[currentCookingStationUpgrade]);
        onTipsChanged.Raise(tipsReference.GetValue());
        //extraCookingStationUpgradesAvailable--;
        currentCookingStationUpgrade++;
        CheckMoney();
    }
    /*public void ApplyUpgrades()
    {
        for (var i = currentCMSUpgrade; i>=0; i--)
        {
            playerReference.GetComponent<PlayerMovement>().MovementSpeed += movementSpeedUpgradeAmount;
        }
        for (var j = currentCMSUpgrade; j >= 0; j--)
        {
            customerMovementSpeedChange.InvokeEvent(AlienMovementSpeedUpgradeAmount);
        }
        for (int j = 0; j < currentSeatingUpgrade; j++)
        {
            tables[j].GetComponent<Table>().UnlockTable();
        }
        for (int i = 0; i <= currentStorageUpgrade; i++)
        {
            foodPickupStation.UpgradeFoodCounterStorage();
        }
        for (int i = 0; i <= currentCookingStationUpgrade; i++)
        {
            kitchen.AddChef();
        }
    }*/
}