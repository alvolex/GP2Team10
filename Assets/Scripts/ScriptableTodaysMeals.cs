using System;
using System.Collections.Generic;
using SOs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Today's meals", menuName = "SO/Today's Meals", order = 0)]
    public class ScriptableTodaysMeals : ScriptableObject
    {
        [SerializeField, Tooltip("How many different options should be available to choose every day")]
        private int amountOfEachFoodEachDay = 2;

        [SerializeField] private List<ScriptableFood> allFood = new List<ScriptableFood>();

        private List<ScriptableFood> allStarters = new List<ScriptableFood>();
        private List<ScriptableFood> allMains = new List<ScriptableFood>();
        private List<ScriptableFood> allDesserts = new List<ScriptableFood>();

        [SerializeField] private List<ScriptableFood> todaysStarters = new List<ScriptableFood>();
        [SerializeField] private List<ScriptableFood> todaysMains = new List<ScriptableFood>();
        [SerializeField] private List<ScriptableFood> todaysDesserts = new List<ScriptableFood>();

        //Getters
        public List<ScriptableFood> TodaysStarters => todaysStarters;
        public List<ScriptableFood> TodaysMains => todaysMains;
        public List<ScriptableFood> TodaysDesserts => todaysDesserts;

        private void OnEnable()
        {
            ClearLists();
            PrepareFoodLists();
            SetTodaysMeals();
        }

        private void ClearLists()
        {
            allStarters = new List<ScriptableFood>();
            allMains = new List<ScriptableFood>();
            allDesserts = new List<ScriptableFood>();
        }

        private void PrepareFoodLists()
        {
            foreach (var food in allFood)
            {
                switch (food.FoodType)
                {
                    case FoodType.Starter:
                        allStarters.Add(food);
                        break;
                    case FoodType.MainCourse:
                        allMains.Add(food);
                        break;
                    case FoodType.Dessert:
                        allDesserts.Add(food);
                        break;
                    case FoodType.NotOrdered:
                        Debug.Log("This food has the wrong food type, this should not be set tot \"Not Ordered\"");
                        break;
                    default:
                        Debug.Log("Ah shiet, we've messed with the time-space continuum");
                        break;
                }
            }
        }

        private void SetTodaysMeals()
        {
            ClearYesterdaysMeals();

            //Set todays starters
            for (var i = 0; i < Mathf.Clamp(amountOfEachFoodEachDay, 0, allStarters.Count); i++)
                while (true)
                {
                    var randomFoodIndex = Random.Range(0, allStarters.Count);
                    if (todaysStarters.Contains(allStarters[randomFoodIndex])) continue;

                    todaysStarters.Add(allStarters[randomFoodIndex]);
                    break;
                }

            //Set todays mains
            for (var i = 0; i < Mathf.Clamp(amountOfEachFoodEachDay, 0, allMains.Count); i++)
                while (true)
                {
                    var randomFoodIndex = Random.Range(0, allMains.Count);
                    if (todaysMains.Contains(allMains[randomFoodIndex])) continue;

                    todaysMains.Add(allMains[randomFoodIndex]);
                    break;
                }

            //Set todays desserts
            for (var i = 0; i < Mathf.Clamp(amountOfEachFoodEachDay, 0, allDesserts.Count); i++)
                while (true)
                {
                    var randomFoodIndex = Random.Range(0, allDesserts.Count);
                    if (todaysDesserts.Contains(allDesserts[randomFoodIndex])) continue;

                    todaysDesserts.Add(allDesserts[randomFoodIndex]);
                    break;
                }
        }

        private void ClearYesterdaysMeals()
        {
            todaysStarters = new List<ScriptableFood>();
            todaysMains = new List<ScriptableFood>();
            todaysDesserts = new List<ScriptableFood>();
        }
    }
}