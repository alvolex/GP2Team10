using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class DeliverFood : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    [SerializeField] private Image foodSpriteRenderer;

    [SerializeField] private ScriptablePlayerCurrentAction currentAction;
    

    private AlienEatFood customer;
    private bool canDeliverFood;

    private Order curOrder;

    private void Start()
    {
        canDeliverFood = false;
    }

    private void Update()
    {
        if (!canDeliverFood || customer == null || curOrder == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            customer.DeliverFood(curOrder);
            FoodDelivered();
        }
    }

    private void FoodDelivered()
    {
        curOrder = null;
        plate.SetActive(false);
        currentAction.CurrentAction = CurrentAction.None;
    }

    public void PickupFood(Order order)
    {
        curOrder = order;
        plate.SetActive(true);
        foodSpriteRenderer.sprite = order.GetFoodSprite();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out AlienEatFood curCustomer)) return;

        customer = curCustomer;
        canDeliverFood = true;
    }
}