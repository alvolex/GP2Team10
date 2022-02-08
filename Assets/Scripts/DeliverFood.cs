using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Scriptables;
using UnityEngine;
using UnityEngine.UI;

public class DeliverFood : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    [SerializeField] private Image foodSpriteRenderer;

    [SerializeField] private ScriptablePlayerCurrentAction currentAction;
    
    
    [Header("Tutorial")]
    [SerializeField] private ScriptableTutorialEvent tutorialEvent;
    [SerializeField] private ScriptableTutorialText orderDeliveredText;

    [SerializeField] private List<AlienEatFood> closeAlienList = new List<AlienEatFood>();


    private AlienEatFood customerEatFood;
    private bool canDeliverFood;
    private bool canDestroyFood = false;

    private Order curOrder;

    private PlayerMovement playerMovement;

    private void Start()
    {
        canDeliverFood = false;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (canDestroyFood && Input.GetKeyDown(KeyCode.Space))
        {
            //Delivered to the trash, amirite?
            FoodDelivered();
        }

        if (!canDeliverFood || customerEatFood == null || curOrder == null || customerEatFood.Of.MyOrder == null ||
            customerEatFood.HasRecievedFood) return;
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AlienEatFood closestAlien = closeAlienList[0];
            float dist = Vector3.Distance(transform.position, closestAlien.transform.position); //Get dist to first alien
            
            foreach (var alien in closeAlienList)
            {
                var distToCheck = Vector3.Distance(transform.position, alien.transform.position);

                if (distToCheck < dist)
                {
                    closestAlien = alien;
                }
            }

            closeAlienList.Remove(closestAlien);
            closestAlien.DeliverFood(curOrder);
            closestAlien.GetComponent<AlienAttributes>().ChangeCustomerState();
            //customerEatFood.DeliverFood(curOrder);
            FoodDelivered();
        }
    }

    private void FoodDelivered()
    {
        //Tutorial food is delivered
        tutorialEvent.InvokeEvent(orderDeliveredText);


        curOrder = null;
        plate.SetActive(false);
        playerMovement.hasPlate = false;
        currentAction.CurrentAction = CurrentAction.None;
    }

    public void PickupFood(Order order)
    {
        curOrder = order;
        plate.SetActive(true);
        playerMovement.hasPlate = true;
        foodSpriteRenderer.sprite = order.GetFoodSprite();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GarbageCan _))
        {
            canDestroyFood = true;
        }
        
        if (!other.TryGetComponent(out AlienEatFood curCustomer)) return;

        if (curCustomer.Of.HasOrdered)
        {
            closeAlienList.Add(curCustomer);
            customerEatFood = curCustomer;
            canDeliverFood = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AlienEatFood cust))
        {
            if (closeAlienList.Contains(cust))
            {
                closeAlienList.Remove(cust);
            }

            if (closeAlienList.Count == 0)
            {
                canDeliverFood = false;
            }
        }
        
        if (other.TryGetComponent(out GarbageCan _))
        {
            canDestroyFood = false;
        }
    }
}