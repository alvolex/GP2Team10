using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TakeOrderFromCustomer : MonoBehaviour
{
    //todo rename script to be "HandleCustomerOrders"

    [SerializeField] private ScriptablePlayerCurrentAction currentAction;

    private Queue<Order> allCurrentOrders = new Queue<Order>();

    private OrderFood of;
    private Kitchen kitchen;

    private bool canTakeOrder;
    private bool canLeaveOrdersToKitchen;

    //Makes the trigger more reliable when there are multiple stacked triggers.
    private List<OrderFood> ofList = new List<OrderFood>();

    private void Start()
    {
        canTakeOrder = false;
        canLeaveOrdersToKitchen = false;
    }

    private void Update()
    {
        //todo This is one ugly if statement
        if (of != null && !of.HasOrdered && canTakeOrder && (currentAction.CurrentAction == CurrentAction.None ||
                                                             currentAction.CurrentAction == CurrentAction.HandlingOrder))
        {
            HandleTakeOrderFromCustomer();
        }

        if (canLeaveOrdersToKitchen)
        {
            HandleLeaveOrdersToKitchen();
        }
    }

    private void HandleTakeOrderFromCustomer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && of.ReadyToOrder /*&& canTakeOrder*/)
        {
            of.StartOrderProcess();
            
            //If another customer is close by, make it so that we can take their order by pressing space again
            ofList.Remove(of);
            if (ofList.Count != 0)
            {
                of = ofList[0];
            }
        }
    }

    public void QueueUpOrder(Order order)
    {
        //Add food to all our current orders
        allCurrentOrders.Enqueue(order);
    }

    private void HandleLeaveOrdersToKitchen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (allCurrentOrders.Count == 0 || kitchen == null) return;

            kitchen.OrdersToCook = allCurrentOrders;
            allCurrentOrders = new Queue<Order>(); //Empty queue
            currentAction.CurrentAction = CurrentAction.None;
        }
    }
    
    //Todo experiment with overlapspheres, might be more performant, currently it's not a noticeable performance hit, maybe range-checking like in "TableAssigner" is a better option
    //Using list makes the trigger more reliable when there are multiple stacked triggers. 
    private void OnTriggerEnter(Collider other)
    {
        //Take order from customer
        if ((currentAction.CurrentAction == CurrentAction.None || currentAction.CurrentAction == CurrentAction.HandlingOrder) && other.TryGetComponent(out OrderFood orderFood))
        {
            if (orderFood.HasOrdered)
            {
                return;
            }
            
            if (!ofList.Contains(orderFood))
            {
                ofList.Add(orderFood);
            }

            of = orderFood;
            canTakeOrder = true;
            of.PlayerRef = this;
        }
        
        //Leave orders at the kitchen
        if (currentAction.CurrentAction == CurrentAction.HandlingOrder && other.TryGetComponent(out Kitchen kitchenCollided))
        {
            canLeaveOrdersToKitchen = true;
            kitchen = kitchenCollided;
        }
    }
    
    //Using list makes the trigger more reliable when there are multiple stacked triggers. 
    private void OnTriggerExit(Collider other)
    {
        if (!canTakeOrder && !canLeaveOrdersToKitchen) return;

        if (other.GetComponent<Kitchen>())
        {
            canLeaveOrdersToKitchen = false;
        }

        var removeFood = other.GetComponent<OrderFood>();
        if (removeFood == null) return;
        
        ofList.Remove(removeFood);
        if (ofList.Count == 0)
        {
            canTakeOrder = false;
        }
        else if (ofList.Count == 1)
        {
            of = ofList[0];
        }
    }
}