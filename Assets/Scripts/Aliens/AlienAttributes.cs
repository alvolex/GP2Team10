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

    public void CheckAllergies(ScriptableFood foodToCheck)
    {
        foreach (var VARIABLE in foodToCheck.Allergies)
        {
            foreach (var VARIABLE2 in allergy)
            {
                if (VARIABLE == VARIABLE2)
                {
                    Debug.Log("Allergy spotted, killed customer");
                    Destroy(gameObject);
                    return;
                }
            }
            Debug.Log(VARIABLE);
            
            reputationReference.ApplyChange(+maxRep);
            onReputationChanged.Raise(reputation.Value);
            
            tipsReference.ApplyChange(+maxTip);
            onTipsChanged.Raise(tips.Value);
        }
    }
}


