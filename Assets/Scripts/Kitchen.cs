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
    
    private int amountOfChefs = 1;

    private Queue<Order> ordersToCook = new Queue<Order>();
    private bool isCooking;
    private Order currentlyCooking;
    int mealsPreparedCurrently = 0;
    private FoodProgressUI foodProgressUI;

    private void Start()
    {
        isCooking = false;
        foodProgressUI = GetComponent<FoodProgressUI>();
    }

    //Getters & setters
    public Queue<Order> OrdersToCook
    {
        get => ordersToCook;
        set => UpdateOrdersToCook(value);
    }

    private void UpdateOrdersToCook(Queue<Order> newOrders)
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
    
    public void AddChef()
    {
        amountOfChefs++;
    }

    
    private void StartCooking()
    {
        AudioManager.Instance.PlayOrderStartSFX();

        for (int i = 0; i < amountOfChefs; i++)
        {
            StartCoroutine(CookFood());
        }
        
    }

    private void UpdateKitchenUI()
    {
        
        foodThatIsCurrentlyCooked.sprite = currentlyCooking.GetFoodSprite();
    }

    IEnumerator CookFood()
    {
        //todo check if there is any open space on the FoodPickupStation for another meal to be placed, otherwise stop cooking until there is room
        
        if ( amountOfChefs < mealsPreparedCurrently ) yield break; //Early return if we're already cooking
        mealsPreparedCurrently++;
        
        isCooking = true; //Make sure we can't start the co-routine again if it's already running
        while (ordersToCook.Count != 0)
        {
            if (foodPickupStation.DoesCounterHaveEnoughSpace())
            {
                currentlyCooking = ordersToCook.Dequeue();
                Debug.Log("Now cooking: " + currentlyCooking.SelectedFoodItem.FoodName);
                //Update sprite on the kitchen & setup progress UI
                UpdateKitchenUI();
                foodProgressUI.UpdateFoodImageAndProgress(currentlyCooking.GetFoodSprite(), currentlyCooking.SelectedFoodItem.TimeToCookFood, currentlyCooking.SelectedFoodItem.TimeBeforeFoodSpoils, currentlyCooking);

                yield return new WaitForSeconds(currentlyCooking.SelectedFoodItem.TimeToCookFood);
                foodPickupStation.FoodIsReady(currentlyCooking);
                
                //Tutorial Stuff
                if (Tutorial.instance != null)
                {
                    if (Tutorial.instance.GameState.foodReadyToDeliverTutorial)
                    {
                        Tutorial.instance.TurnOnAndMoveSpotlight();
                        Tutorial.instance.GameState.foodReadyToDeliverTutorial = false;
                    }
                }
                
            }
            else
            {
                Debug.Log("Waiting for space to be made");
                yield return new WaitForSeconds(0.2f);
            }
            
        }

        foodThatIsCurrentlyCooked.sprite = imgWhenNoFoodIsBeingCooked;
        mealsPreparedCurrently--;
        isCooking = false;
    }
    
}
