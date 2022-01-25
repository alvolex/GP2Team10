using System;
using SOs;
using UnityEngine;

namespace DefaultNamespace
{
    public class Order
    {
        private readonly FoodType foodType = FoodType.NotOrdered;
        private readonly Customer customerWhoOrderedTheFood;
        private readonly Sprite foodSprite;

        private ScriptableFood selectedFoodItem;
        public ScriptableFood SelectedFoodItem
        {
            get => selectedFoodItem;
            set => selectedFoodItem = value;
        }

        public Order(FoodType orderedFood, Customer customer, Sprite foodImg, ScriptableFood scriptableFood)
        {
            foodType = orderedFood;
            customerWhoOrderedTheFood = customer;
            foodSprite = foodImg;
            selectedFoodItem = scriptableFood;
        }

        public FoodType GetFood()
        {
            return foodType;
        }

        public Customer GetCustomerWhoOrdered()
        {
            return customerWhoOrderedTheFood;
        }

        public Sprite GetFoodSprite()
        {
            return foodSprite;
        }
    }
}