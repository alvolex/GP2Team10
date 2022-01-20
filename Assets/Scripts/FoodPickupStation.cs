using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class FoodPickupStation : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScriptablePlayerCurrentAction curAction;

    private Queue<Food> foodDisplayQueue = new Queue<Food>();
    private List<Image> spriteRenderers = new List<Image>();

    //testing
    private Sprite emptyPlateSprite;
    private bool canPickupFood;

    private CarryFood playerCarry;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<Image>().ToList();
        emptyPlateSprite = spriteRenderers[0].sprite;
        canPickupFood = false;
    }

    private void Update()
    {
        if (canPickupFood && foodDisplayQueue.Count != 0 && Input.GetKeyDown(KeyCode.Space))
        {
            PickupFood();
        }
    }

    public void FoodIsReady(Food food)
    {
        foodDisplayQueue.Enqueue(food);
        UpdateFoodPlatesOnCounter();
    }

    public void UpdateFoodPlatesOnCounter()
    {
        //Reset all the plates to empty before every iteration because im lazy atm
        //todo maybe fix this? idk if necessary (read comment above)
        for (int j = 0; j < 3; j++)
        {
            spriteRenderers[j].sprite = emptyPlateSprite;
        }

        int index = Mathf.Clamp(foodDisplayQueue.Count, 0, 3);
        for (int j = 0; j < index; j++)
        {
            spriteRenderers[j].sprite = foodDisplayQueue.ElementAt(j).GetFoodSprite();
        }
    }

    void PickupFood()
    {
        var foodToPickup = foodDisplayQueue.Dequeue();
        UpdateFoodPlatesOnCounter();

        Debug.Log("Food has been picked up: " + foodToPickup.GetFood());

        Debug.Log(playerCarry);

        if (playerCarry != null)
        {
            playerCarry.PickupFood(foodToPickup); //Send the food over to the player
            curAction.CurrentAction = CurrentAction.DeliveringFood;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (curAction.CurrentAction != CurrentAction.None || !other.TryGetComponent(out CarryFood pc)) return;

        playerCarry = pc;
        canPickupFood = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canPickupFood = false;
    }
}