using System;
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
    
    [Header("Waiting Times")]
    [SerializeField] private float maxWaitingToBeSeatedTime;
    [SerializeField] private float maxWaitingToOrderTime;
    [SerializeField] private float maxWaitingForOrderTime;

    [Header("Alex's Stupid Over head pop-up Event :^)")] 
    [SerializeField] private ScriptableMoneyPopupEvent moneyPopupEvent;
    [SerializeField] private ScriptableSimpleEvent customerStateChange;
    

    enum customerState
    {
        WaitingToBeSeated,
        WaitingToOrder,
        WaitingForFood,
    }

    private customerState currentCustomerState;
    
    public event Action<Customer> customerHasDied;
    
    private void Start()
    {
        customerStateChange.ScriptableEvent += ChangeCustomerState;
        currentCustomerState = customerState.WaitingToBeSeated;
    }

    private void OnDestroy()
    {
        customerStateChange.ScriptableEvent -= ChangeCustomerState;
    }

    private void ChangeCustomerState()
    {
        if (GetComponent<Customer>().IsSeated)
        {
            currentCustomerState = customerState.WaitingToOrder;
        }
    }
    private void Update()
    {
        if (currentCustomerState  == customerState.WaitingToBeSeated)
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
                    Destroy(gameObject);
                    AudioManager.Instance.PlayAlienExplodeSFX();
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
        
        AudioManager.Instance.PlayGetMoneySFX();
        
        moneyPopupEvent.InvokeEvent(maxTip, GetComponent<Customer>());

    }
    
    
}
            
            

