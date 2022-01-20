using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class CarryFood : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    [SerializeField] private Image foodSpriteRenderer;

    public void PickupFood(Food food)
    {
        plate.SetActive(true);
        foodSpriteRenderer.sprite = food.GetFoodSprite();
    }

}
