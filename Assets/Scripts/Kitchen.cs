using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    [SerializeField] private Image foodThatIsCurrentlyCooked;
    [SerializeField] private Sprite imgWhenNoFoodIsBeingCooked;
    

    //todo should create a scriptable event for this instead of directly referencing the other script
    [SerializeField] private FoodPickupStation foodPickupStation;

    private Queue<Food> ordersToCook = new Queue<Food>();
    private bool isCooking;
    private Food currentlyCooking;

    private void Start()
    {
        isCooking = false;
    }

    //Getters & setters
    public Queue<Food> OrdersToCook
    {
        get => ordersToCook;
        set => UpdateOrdersToCook(value);
    }

    private void UpdateOrdersToCook(Queue<Food> newOrders)
    {
        //Add new orders to the end of the queue if the queue isn't empty
        if (ordersToCook.Count != 0)
        {
            foreach (var order in newOrders)
            {
                ordersToCook.Enqueue(order);
            }
        }
        else
        {
            ordersToCook = newOrders;
        }

        StartCooking();
    }

    
    private void StartCooking()
    {
        StartCoroutine(CookFood());
    }

    private void UpdateKitchenUI()
    {
        foodThatIsCurrentlyCooked.sprite = currentlyCooking.GetFoodSprite();
    }

    IEnumerator CookFood()
    {
        //todo check if there is any open space on the FoodPickupStation for another meal to be placed, otherwise stop cooking until there is room
        
        if (isCooking) yield break; //Early return if we're already cooking
        
        isCooking = true; //Make sure we can't start the co-routine again if it's already running
        while (ordersToCook.Count != 0)
        {
            currentlyCooking = ordersToCook.Dequeue();
            UpdateKitchenUI();
                
            yield return new WaitForSeconds(5f);
            foodPickupStation.FoodIsReady(currentlyCooking);
        }

        foodThatIsCurrentlyCooked.sprite = imgWhenNoFoodIsBeingCooked;
        isCooking = false;
    }
    
}
