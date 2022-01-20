using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using UnityEngine;
using Variables;

public class AlienAttributes : MonoBehaviour
{

    [SerializeField] private IntVariable reputation;
    [SerializeField] private IntReference reputationReference;
    [SerializeField] private ScriptableEventIntReference onReputationChanged;

    public Ingredients.Allergy[] allergy;
    [SerializeField] private int maxRep;
    [SerializeField] private int maxTip;
    [SerializeField] private int maxTime;
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("in food");

        foreach (var VARIABLE in other.gameObject.GetComponent<TestMeal>().allergy)
        {
            foreach (var VARIABLE2 in allergy)
            {
                if (VARIABLE == VARIABLE2)
                {
                    Debug.Log("Allergy spotted, killed customer");
                    return;
                }
            }
            Debug.Log(VARIABLE);
            
            reputationReference.ApplyChange(+maxRep);
            onReputationChanged.Raise(reputation.Value);
            
            
        }
    }
}


