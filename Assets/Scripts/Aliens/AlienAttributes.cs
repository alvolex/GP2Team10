using System;
using System.Collections;
using ScriptableEvents;
using Scriptables;
using SOs;
using UnityEngine;
using Variables;


[Serializable]
public class AlienAttributes : MonoBehaviour
{
    //If we are having a type of alien, we can register it here for the saveData

    [SerializeField] private IntVariable reputation;
    [SerializeField] private IntVariable tips;

    [Header("Reputation: ")]
    [SerializeField] private IntReference reputationReference;
    [SerializeField] private ScriptableEventIntReference onReputationChanged;
    
    [Header("Tips: ")]
    [SerializeField] private IntReference tipsReference;
    [SerializeField] private ScriptableEventIntReference onTipsChanged;

    [Header("AliensFed: ")]
    [SerializeField] private IntReference aliensFedReference;
    [SerializeField] private ScriptableEventIntReference onAlienFed;
    
    [Header("Allergens fed: ")]
    [SerializeField] private IntReference allergensFedReference;
    [SerializeField] private ScriptableEventIntReference onAllergenFed;
   
    [Header("Starter orders: ")]
    [SerializeField] private IntReference startersFed;
    [SerializeField] private ScriptableEventIntReference onStarterFed;
    
    [Header("Maincourse orders: ")]
    [SerializeField] private IntReference maincourseFed;
    [SerializeField] private ScriptableEventIntReference onMaincourseFed;
    
    [Header("Dessert orders: ")]
    [SerializeField] private IntReference dessertsFed;
    [SerializeField] private ScriptableEventIntReference onDessertFed;
    
    

    public Ingredients.Allergy[] allergy;
    [SerializeField] private int maxRep;
    [SerializeField] private int maxTip;
    
    [Header("Negative rep from killing, put this without the minus sign")]
    [SerializeField] private int negativeRepFromKilling;
    
    
    [Header("Waiting Times")]
    [SerializeField] private float maxWaitingToBeSeatedTime;
    [SerializeField] private float maxWaitingToOrderTime;
    [SerializeField] private float maxWaitingForOrderTime;

    [Header("Alex's Stupid Over head pop-up Event :^)")] 
    [SerializeField] private ScriptableMoneyPopupEvent moneyPopupEvent;
    [SerializeField] private ScriptableMoneyPopupEvent reputationPopupEvent;
    [SerializeField] private ScriptableSimpleEvent customerStateChange;

    private NegativeReputationPrompt promptPos;


    enum customerState
    {
        WaitingToBeSeated,
        WaitingToOrder,
        WaitingForFood,
    }

    private customerState currentCustomerState = customerState.WaitingToBeSeated;
    
    public event Action<Customer> customerHasDied;
    
    private void Start()
    {
        customerStateChange.ScriptableEvent += ChangeCustomerState;
        currentCustomerState = customerState.WaitingToBeSeated;
        
        StartCoroutine(CustomerWaitTimer(maxWaitingToBeSeatedTime, customerState.WaitingToBeSeated));

        promptPos = FindObjectOfType<NegativeReputationPrompt>();
    }
    private void OnDestroy()
    {
        customerStateChange.ScriptableEvent -= ChangeCustomerState;
    }
    
    public void ChangeCustomerState()
    {
        //StopAllCoroutines();
        
        switch (currentCustomerState)
        {
            case customerState.WaitingToBeSeated:
                currentCustomerState = customerState.WaitingToOrder;
                StartCoroutine(CustomerWaitTimer(maxWaitingToOrderTime, customerState.WaitingToOrder));
                break;
            case customerState.WaitingToOrder:
                currentCustomerState = customerState.WaitingForFood;
                StartCoroutine(CustomerWaitTimer(maxWaitingForOrderTime, customerState.WaitingForFood));
                break;
        }
    }
    private void Update()
    {
        /*if (currentCustomerState  == customerState.WaitingToBeSeated)
        {
            maxWaitingToBeSeatedTime -= Time.deltaTime;
        }
        if (currentCustomerState  == customerState.WaitingToOrder)
        {
            maxWaitingToOrderTime -= Time.deltaTime;
        }
        if (currentCustomerState  == customerState.WaitingForFood)
        {
            maxWaitingForOrderTime -= Time.deltaTime;
        }*/
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            startersFed.ApplyChange(+1);
            onStarterFed.Raise(startersFed.GetValue());
            dessertsFed.ApplyChange(+1);
            onDessertFed.Raise(dessertsFed.GetValue());
            aliensFedReference.ApplyChange(+1);
            onAlienFed.Raise(aliensFedReference.GetValue());
        }
    }
    public void CheckAllergies(ScriptableFood foodToCheck)
    {
        foreach (var allergyInFood in foodToCheck.Allergies)
        {
            foreach (var alienAllergy in allergy)
            {
                if (allergyInFood == alienAllergy)
                {
                    //TODO separate the allergy killing from the allergy just making them sick
                    Debug.Log("Allergy spotted, killed customer");
                    CustomerIsAllergic();
                    
                    promptPos.HandleMoneyPopup(transform.position, negativeRepFromKilling);
                    Debug.Log(promptPos);
                    //POSITION HERE
                    
                    Destroy(gameObject);
                    AudioManager.Instance.PlayAlienExplodeSFX();
                    reputationReference.ApplyChange(-negativeRepFromKilling);
                    onReputationChanged.Raise(reputationReference.GetValue());
                    customerHasDied?.Invoke(gameObject.GetComponent<Customer>());
                    return;
                }
            }
        }
        FoodIsEdible(foodToCheck);
    }
    private void CustomerIsAllergic()
    {
        allergensFedReference.ApplyChange(+1);
        onAllergenFed.Raise(allergensFedReference.GetValue());
    }
    void FoodIsEdible(ScriptableFood foodToCheck)
    {
        if (foodToCheck.FoodType == FoodType.Starter)
        {
            startersFed.ApplyChange(+1);
            onStarterFed.Raise(startersFed.GetValue());
        }
        if (foodToCheck.FoodType == FoodType.MainCourse)
        {
            maincourseFed.ApplyChange(+1);
            onMaincourseFed.Raise(maincourseFed.GetValue());
        }
        if (foodToCheck.FoodType == FoodType.Dessert)
        {
            dessertsFed.ApplyChange(+1);
            onDessertFed.Raise(dessertsFed.GetValue());
        }
        
        aliensFedReference.ApplyChange(+1);
        onAlienFed.Raise(aliensFedReference.GetValue());
        
        reputationReference.ApplyChange(+maxRep);
        onReputationChanged.Raise(aliensFedReference.GetValue());

        tipsReference.ApplyChange(+maxTip);
        onTipsChanged.Raise(aliensFedReference.GetValue());  
        
        Debug.Log($"Recieved {maxTip} dollal");
        
        AudioManager.Instance.PlayGetMoneySFX();
        
        moneyPopupEvent.InvokeEvent(maxTip, GetComponent<Customer>());
        //reputationPopupEvent.InvokeEvent(maxRep, GetComponent<Customer>());
        
    }

    IEnumerator CustomerWaitTimer(float timeToWwait, customerState state)
    {
        var stateOnStart = state;
        yield return new WaitForSeconds(timeToWwait);

        if (stateOnStart == currentCustomerState)
        {
            GetComponent<Customer>().ExitRestaurant();
        }
    }
}
            
            

