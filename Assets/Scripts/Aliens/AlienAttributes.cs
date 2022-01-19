using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAttributes : MonoBehaviour
{

    public Ingredients.Allergy[] allergy;
    [SerializeField] private int maxRep;
    [SerializeField] private int maxTip;
    [SerializeField] private int maxTime;
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("in food");

        foreach (var VARIABLE in other.gameObject.GetComponent<TestMeal>().allergy)
        {
            Debug.Log(VARIABLE);
        }
    }
}


