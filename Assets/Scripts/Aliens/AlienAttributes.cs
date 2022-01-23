using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using SOs;
using UnityEngine;
using Variables;

public class AlienAttributes : MonoBehaviour
{

    [SerializeField] private IntVariable reputation;
    [SerializeField] private IntReference reputationReference;
    [SerializeField] private ScriptableEventIntReference onReputationChanged;

    [SerializeField] private IntVariable tips;
    [SerializeField] private IntReference tipsReference;
    [SerializeField] private ScriptableEventIntReference onTipsChanged;

    public Ingredients.Allergy[] allergy;
    [SerializeField] private int maxRep;
    [SerializeField] private int maxTip;
    [SerializeField] private int maxTime;

    public event Action<Customer> customerHasDied;

    public void CheckAllergies(ScriptableFood foodToCheck)
    {
        foreach (var allergyInFood in foodToCheck.Allergies)
        {
            foreach (var alienAllergy in allergy)
            {
                if (allergyInFood == alienAllergy)
                {
                    Debug.Log("Allergy spotted, killed customer");
                    Destroy(gameObject);
                    customerHasDied?.Invoke(gameObject.GetComponent<Customer>());
                    return;
                }
            }

            reputationReference.ApplyChange(+maxRep);
            onReputationChanged.Raise(reputation.Value);
            
            tipsReference.ApplyChange(+maxTip);
            onTipsChanged.Raise(tips.Value);
        }
    }
}


