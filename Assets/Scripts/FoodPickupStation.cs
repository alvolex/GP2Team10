using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Scriptables;
using UnityEngine;
using UnityEngine.UI;

public class FoodPickupStation : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private ScriptablePlayerCurrentAction curAction;
    
    [Header("Events")] 
    [SerializeField] private ScriptableSimpleEvent foodPickedUp;
    [SerializeField] private ScriptableSimpleEvent destroyFoodWhenThrown;
    
    [Header("Tutorial")]
    [SerializeField] private ScriptableTutorialEvent tutorialEvent;
    [SerializeField] private ScriptableTutorialText foodAtTheCounterText;

    private Queue<Order> foodDisplayQueue = new Queue<Order>();
    private List<Image> spriteRenderers = new List<Image>();
    private int unlockedFoodSlots = 3;

    //testing
    private Sprite emptyPlateSprite;
    private bool canPickupFood;

    private DeliverFood playerCarry;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<Image>().ToList();
        emptyPlateSprite = spriteRenderers[0].sprite;
        canPickupFood = false;
        UpdateFoodPlatesOnCounter();

        destroyFoodWhenThrown.ScriptableEvent += DestroyFood;
    }

    private void OnDestroy()
    {
        destroyFoodWhenThrown.ScriptableEvent -= DestroyFood;
    }

    private void Update()
    {
        if (canPickupFood && foodDisplayQueue.Count != 0 && Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayPickupPlateSFX();
            foodPickedUp.InvokeEvent();
            PickupFood();
        }
    }

    public void UpgradeFoodCounterStorage()
    {
        unlockedFoodSlots++;
        UpdateFoodPlatesOnCounter();
    }

    public bool DoesCounterHaveEnoughSpace()
    {
        return foodDisplayQueue.Count < unlockedFoodSlots;
    }

    public void FoodIsReady(Order food)
    {
        AudioManager.Instance.PlayOrderCompleteSFX();
        foodDisplayQueue.Enqueue(food);
        UpdateFoodPlatesOnCounter();
    }

    public void UpdateFoodPlatesOnCounter()
    {
        //Reset all the plates to empty before every iteration because im lazy atm
        //todo maybe fix this? idk if necessary (read comment above)
        for (int j = 0; j < unlockedFoodSlots; j++)
        {
            spriteRenderers[j].gameObject.SetActive(true);
            spriteRenderers[j].sprite = emptyPlateSprite;
        }
        
        for (int j = unlockedFoodSlots; j < spriteRenderers.Count; j++)
        {
            spriteRenderers[j].gameObject.SetActive(false);
            spriteRenderers[j].sprite = emptyPlateSprite;
        }

        int index = Mathf.Clamp(foodDisplayQueue.Count, 0, unlockedFoodSlots);
        for (int j = 0; j < index; j++)
        {
            spriteRenderers[j].sprite = foodDisplayQueue.ElementAt(j).GetFoodSprite();
        }
    }

    void DestroyFood()
    {
        if (foodDisplayQueue.Count == 0) return;

        var foodToPickup = foodDisplayQueue.Dequeue();
        foodToPickup.HasBeenPickedUp = true;
        
        UpdateFoodPlatesOnCounter();
    }

    void PickupFood()
    {
        if (foodDisplayQueue.Count == 0) return;
        
        var foodToPickup = foodDisplayQueue.Dequeue();
        UpdateFoodPlatesOnCounter();

        foodToPickup.HasBeenPickedUp = true;
        
        //Tutorial Stuff
        tutorialEvent.InvokeEvent(foodAtTheCounterText);

        //Debug.Log("Food has been picked up: " + foodToPickup.GetFood());

        if (playerCarry != null)
        {
            playerCarry.PickupFood(foodToPickup); //Send the food over to the player
            curAction.CurrentAction = CurrentAction.DeliveringFood;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (curAction.CurrentAction != CurrentAction.None || !other.TryGetComponent(out DeliverFood pc)) return;

        playerCarry = pc;
        canPickupFood = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canPickupFood = false;
    }
}