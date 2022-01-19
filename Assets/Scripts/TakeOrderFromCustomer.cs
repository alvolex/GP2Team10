using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TakeOrderFromCustomer : MonoBehaviour
{
    [SerializeField] private ScriptablePlayerCurrentAction currentAction;

    private Queue<Food> allCurrentOrders = new Queue<Food>();

    private Customer customer;
    private OrderFood of;
    private Kitchen kitchen;
    
    private bool canTakeOrder;
    private bool canLeaveOrdersToKitchen;
    
    private void Start()
    {
        canTakeOrder = false;
        canLeaveOrdersToKitchen = false;
        customer = GetComponent<Customer>();
    }

    private void Update()
    {
        if (of != null)
        {
            HandleTakeOrderFromCustomer();
        };

        if (canLeaveOrdersToKitchen)
        {
            HandleLeaveOrdersToKitchen();
        }
    }

    private void HandleLeaveOrdersToKitchen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (allCurrentOrders.Count == 0) return;

            kitchen.OrdersToCook = allCurrentOrders;
            allCurrentOrders = new Queue<Food>(); //Empty queue
        }
    }

    private void HandleTakeOrderFromCustomer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && of.ReadyToOrder && canTakeOrder)
        {
            of.OrderUIImage.SetActive(false); //Disable the order food pop-up since we've now taken the order
            currentAction.CurrentAction = CurrentAction.HandlingOrder;

            //Add food to all our current orders
            allCurrentOrders.Enqueue(of.MyOrder);

            Debug.Log(of.MyOrder.GetFood());
            of.GetComponent<Customer>().StartEatingFood();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out of))
        {
            canTakeOrder = true;
        }

        if (currentAction.CurrentAction == CurrentAction.HandlingOrder && other.TryGetComponent(out kitchen))
        {
            canLeaveOrdersToKitchen = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        canTakeOrder = false;
        canLeaveOrdersToKitchen = false;
    }
}
