using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TakeOrderFromCustomer : MonoBehaviour
{
    [SerializeField] private ScriptablePlayerCurrentAction currentAction;
    
    private Customer customer;
    private OrderFood of;
    
    private bool canTakeOrder;
    
    private void Start()
    {
        canTakeOrder = false;
        customer = GetComponent<Customer>();
    }

    private void Update()
    {
        if (of == null) return;

        if (Input.GetKeyDown(KeyCode.Space) && of.ReadyToOrder && canTakeOrder)
        {
            of.OrderUIImage.SetActive(false);

            //todo fix this placeholder to just eat the food and leave
            //Next step should be to give the order to the chef, then recieve the food and deliver it 
            currentAction.CurrentAction = CurrentAction.HandlingOrder;
            
            of.GetComponent<Customer>().StartEatingFood();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out of))
        {
            canTakeOrder = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        canTakeOrder = false;
    }
}
