using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Food
    {
        private FoodType foodType = FoodType.NotOrdered;
        private Customer customerWhoOrderedTheFood;
        private Sprite foodSprite; 

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