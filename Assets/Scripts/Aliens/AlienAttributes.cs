using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using SOs;
using UnityEngine;
using Variables;

public class AlienAttributes : MonoBehaviour
{
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
    
    

    public Ingredients.Allergy[] allergy;
    [SerializeField] private int maxRep;
    [SerializeField] private int maxTip;
    [SerializeField] private int maxWaitTime;
    
    
    public event Action<Customer> customerHasDied;
    

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
                    customerHasDied?.Invoke(gameObject.GetComponent<Customer>());
                    return;
                }
            }
            FoodIsEdible();
            
            
        }
    }

    private void CustomerIsAllergic()
    {
        allergensFedReference.ApplyChange(+1);
        onAllergenFed.Raise(allergensFedReference.GetValue());
    }

    void FoodIsEdible()
    {
        aliensFedReference.ApplyChange(+1);
        onAlienFed.Raise(aliensFedReference.GetValue());
        
        reputationReference.ApplyChange(+maxRep);
        onReputationChanged.Raise(aliensFedReference.GetValue());
        
        
        tipsReference.ApplyChange(+maxTip);
        onTipsChanged.Raise(aliensFedReference.GetValue());
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            aliensFedReference.ApplyChange(+1);
            onAlienFed.Raise(aliensFedReference.GetValue());
        }
    }
}


