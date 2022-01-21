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
            //Do dist check instead of "OnTriggerExit" as it kinda borked when using multiple triggers
            //todo sqrMag or throttle how often this is checked
            if (Vector3.Distance(transform.position, of.transform.position) <= 4f)
            {
                HandleTakeOrderFromCustomer();
            }
            else
            {
                canTakeOrder = false;
            }
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
            of.OrderUIImage.SetActive(false); //Disable the order food pop-up since we've now taken the order
            currentAction.CurrentAction = CurrentAction.HandlingOrder;

            //Test stuff for chosing which menu item that we should prepare for the customer
            of.ToggleSelectableFoodItems(); //Shows the different items we can choose from

            of.HasOrdered = true;
            //Add food to all our current orders
            allCurrentOrders.Enqueue(of.MyOrder);

            //of.GetComponent<Customer>().StartEatingFood();
        }
    }

    private void HandleLeaveOrdersToKitchen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (allCurrentOrders.Count == 0) return;

            kitchen.OrdersToCook = allCurrentOrders;
            allCurrentOrders = new Queue<Order>(); //Empty queue
            currentAction.CurrentAction = CurrentAction.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Take order from customer
        if (other.TryGetComponent(out of) && (currentAction.CurrentAction == CurrentAction.None || currentAction.CurrentAction == CurrentAction.HandlingOrder))
        {
            canTakeOrder = true;
        }
        
        //Leave orders at the kitchen
        if (currentAction.CurrentAction == CurrentAction.HandlingOrder && other.TryGetComponent(out kitchen))
        {
            canLeaveOrdersToKitchen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //canTakeOrder = false;
        canLeaveOrdersToKitchen = false;
    }
}