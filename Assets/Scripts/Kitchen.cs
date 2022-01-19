using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    [SerializeField] private Image foodThatIsCurrentlyCooked;
    
    private Queue<Food> ordersToCook = new Queue<Food>();

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

        UpdateKitchenUI();
    }

    private void UpdateKitchenUI()
    {
        //Debug
        foreach (var food in ordersToCook)
        {
            Debug.Log(food.GetFood());
        }
        
        //todo add a time element to the food and spawn in food in the world when the timer is complete, then jut dequeue and start with the next food item
        var curFood = ordersToCook.Dequeue();

        foodThatIsCurrentlyCooked.sprite = curFood.GetFoodSprite();
    }
}
