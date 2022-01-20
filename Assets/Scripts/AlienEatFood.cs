using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AlienEatFood : MonoBehaviour
{
    [SerializeField] private string testeronis;

    public void DeliverFood(Order order)
    {
        Debug.Log( "Order has been recieved by alien: " + order.GetFood());
        
        testeronis = order.GetFood().ToString();
    }
}
