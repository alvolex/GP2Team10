using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AlienEatFood : MonoBehaviour
{
    private AlienAttributes attributes;
    private Order myOrder;
    private OrderFood of;

    private void Start()
    {
        attributes = GetComponent<AlienAttributes>();
        of = GetComponent<OrderFood>();
    }

    public void DeliverFood(Order order)
    {
        GetMyOrder();
        
        Debug.Log( "Order has been recieved by alien: " + order.GetFood());

        attributes.CheckAllergies(order.SelectedFoodItem);

        if (order == myOrder)
        {
            Debug.Log("I received my order!");
        }
        
        StartEatingFood();
    }

    private void GetMyOrder()
    {
        myOrder = of.MyOrder;
    }

    private void StartEatingFood()
    {
        GetComponent<Customer>().StartEatingFood();
    }
}