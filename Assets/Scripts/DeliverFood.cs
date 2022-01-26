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
    

    private AlienEatFood customerEatFood;
    private bool canDeliverFood;

    private Order curOrder;

    private void Start()
    {
        canDeliverFood = false;
    }

    private void LateUpdate()
    {
        if (!canDeliverFood || customerEatFood == null || curOrder == null || customerEatFood.Of.MyOrder == null || customerEatFood.HasRecievedFood) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            customerEatFood.DeliverFood(curOrder);
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
        customerEatFood = curCustomer;
        canDeliverFood = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canDeliverFood = false;
    }
}