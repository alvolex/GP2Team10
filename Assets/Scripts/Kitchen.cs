using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    [SerializeField] private Image foodThatIsCurrentlyCooked;
    
    private Queue<Food> ordersToCook = new Queue<Food>();

    public Queue<Food> OrdersToCook
    {
        get => ordersToCook;
        set
        {
            ordersToCook = value;
            UpdateKitchenUI();
        }
    }

    private void UpdateKitchenUI()
    {
        var curFood = ordersToCook.Dequeue();
        foodThatIsCurrentlyCooked.sprite = curFood.GetFoodSprite();
    }

    
}
