using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Food
    {
        private readonly FoodType foodType = FoodType.NotOrdered;
        private readonly Customer customerWhoOrderedTheFood;
        private readonly Sprite foodSprite; 

        public Food(FoodType orderedFood, Customer customer, Sprite foodImg)
        {
            foodType = orderedFood;
            customerWhoOrderedTheFood = customer;
            foodSprite = foodImg;
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