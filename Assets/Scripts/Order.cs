using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Order
    {
        private readonly FoodType foodType = FoodType.NotOrdered;
        private readonly Customer customerWhoOrderedTheFood;
        private readonly Sprite foodSprite; 

        public Order(FoodType orderedFood, Customer customer, Sprite foodImg)
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