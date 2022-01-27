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

    private void OnTriggerStay(Collider other)
    {
        //Take order from customer
        if ((currentAction.CurrentAction == CurrentAction.None || currentAction.CurrentAction == CurrentAction.HandlingOrder) && other.TryGetComponent(out of))
        {
            canTakeOrder = true;
            of.PlayerRef = this;
        }
        
        //Leave orders at the kitchen
        if (currentAction.CurrentAction == CurrentAction.HandlingOrder && other.TryGetComponent(out kitchen))
        {
            canLeaveOrdersToKitchen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canLeaveOrdersToKitchen = false;
        canTakeOrder = false;
        of = null;
    }
}