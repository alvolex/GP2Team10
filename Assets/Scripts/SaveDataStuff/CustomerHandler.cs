using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerHandler : MonoBehaviour
{
    
    private AlienData alienData;
    
    
    void Start()
    {
        if (string.IsNullOrEmpty(alienData.alienID))
        {
            alienData.alienID = System.DateTime.Now.ToLongDateString() + System.DateTime.Now.ToLongTimeString()+ Random.Range(0,int.MaxValue).ToString();
            //SaveData.current.aliens.Add(alienData);
        }
        
        
    }

    private void Update()
    {
        alienData.alienPosition = transform.position;
        alienData.alienRotation = transform.rotation;
    }

    public void OnLoad()
    {
        
    }
}
