using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using SOs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum FoodType
{
    Starter,
    MainCourse,
    Dessert,
    NotOrdered
}

public class OrderFood : MonoBehaviour
{
    [SerializeField] private GameObject orderUIImage;
    [SerializeField] private GameObject chooseMenuItemImage;
    [SerializeField] private ScriptableTodaysMeals todaysMeals;

    [Header("Food Ordering")] 
    [SerializeField, Range(1f,3f)] private float foodOrderingRadius = 2f;

    //todo this should probably be moved to it's own classeronis
    [Header("This should probably be moved to a separate class")] 
    [SerializeField] private Image spriteRenderer;
    [SerializeField] private Sprite starterSprite;
    [SerializeField] private Sprite maincourseSprite;
    [SerializeField] private Sprite dessertSprite;

    private SphereCollider sCollider;
    private bool readyToOrder;
    private Array enumArr;

    private List<TMP_Text> menuItemTextList = new List<TMP_Text>();

    //Can be used to check if the ordered food matches what they received
    private Order myOrder;
    public Order MyOrder => myOrder;

    private void Start()
    {
        EnumToArray(); //Convert our FoodTypes into an array
        menuItemTextList = chooseMenuItemImage.GetComponentsInChildren<TMP_Text>().ToList();
    }

    private void EnumToArray()
    {
        Type t = typeof(FoodType);
        enumArr = t.GetEnumValues();
    }

    public bool ReadyToOrder => readyToOrder;
    public GameObject OrderUIImage
    {
        get => orderUIImage;
        set => orderUIImage = value;
    }

    public SphereCollider SCollider
    {
        set => sCollider = value;
    }

    public void ToggleSelectableFoodItems()
    {
        chooseMenuItemImage.SetActive(!chooseMenuItemImage.activeSelf);
    }

    public void Order()
    {
        StartCoroutine(TimeToOrder());
    }

    IEnumerator TimeToOrder()
    {
        //Move to other class..
        FoodType foodToOrder = (FoodType) enumArr.GetValue(Random.Range(0, enumArr.Length - 1)); //Get a random food (remove last since that is "Not ordered"..)

        UpdateMenuItemsTextWithTodaysFood(foodToOrder);

        yield return new WaitForSeconds(Random.Range(5f,8f));
        
        orderUIImage.SetActive(true);
        
        if (foodToOrder == FoodType.Starter)
        {
            spriteRenderer.sprite = starterSprite;
        }
        else if (foodToOrder == FoodType.MainCourse)
        {
            spriteRenderer.sprite = maincourseSprite;
        }
        else if (foodToOrder == FoodType.Dessert)
        {
            spriteRenderer.sprite = dessertSprite;
        }
        //End move to other class shtuff
        
        myOrder = new Order(foodToOrder, GetComponent<Customer>(), spriteRenderer.sprite); //Create the new food and assign the correct data to it
        readyToOrder = true;
        
        //Make the collider bigger again when the alien is seated so that we can handle orders
        sCollider.radius = foodOrderingRadius; //todo this needs tweaking
    }

    private void UpdateMenuItemsTextWithTodaysFood(FoodType foodType)
    {
        List<ScriptableFood> foodToChooseFrom = new List<ScriptableFood>();

        //Get the correct list of today's meals based on what the customer chose
        switch (foodType)
        {
            case FoodType.Starter:
                foodToChooseFrom = todaysMeals.TodaysStarters;
                break;
            case FoodType.MainCourse:
                foodToChooseFrom = todaysMeals.TodaysMains;
                break;
            case FoodType.Dessert:
                foodToChooseFrom = todaysMeals.TodaysDesserts;
                break;
        }
        
        //Update the menu items UI over the customers head with the name of the meals for today
        int textIndex = 0;
        foreach (var food in foodToChooseFrom)
        {
            if (menuItemTextList[textIndex] != null)
            {
                menuItemTextList[textIndex].text = food.FoodName;
            }

            textIndex++;
        }
    }
}