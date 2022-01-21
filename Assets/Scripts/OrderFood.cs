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
    
    [Header("Sprites for the different food types")] 
    [SerializeField] private Image spriteRenderer;
    [SerializeField] private Sprite starterSprite;
    [SerializeField] private Sprite maincourseSprite;
    [SerializeField] private Sprite dessertSprite;

    private SphereCollider sCollider;
    private bool readyToOrder;
    private bool hasOrdered;
    private Array enumArr;
    private ScriptableFood selectedDish;
    private FoodType foodToOrder;

    private List<TMP_Text> menuItemTextList = new List<TMP_Text>();

    //Can be used to check if the ordered food matches what they received
    private Order myOrder;
    public Order MyOrder => myOrder;

    public bool HasOrdered
    {
        get => hasOrdered;
        set => hasOrdered = value;
    }

    private void Start()
    {
        EnumToArray(); //Convert our FoodTypes into an array
        menuItemTextList = chooseMenuItemImage.GetComponentsInChildren<TMP_Text>().ToList();
        hasOrdered = false;
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
        Debug.Log("Hello");
        chooseMenuItemImage.SetActive(!chooseMenuItemImage.activeSelf);
    }

    public void Order()
    {
        StartCoroutine(TimeToOrder());
    }

    IEnumerator TimeToOrder()
    {
        foodToOrder = (FoodType) enumArr.GetValue(Random.Range(0, enumArr.Length - 1)); //Get a random food type that the customer wants to order
        
        //Update the UI that shows the dishes with the correct info
        UpdateMenuItemsTextWithTodaysFood(foodToOrder);

        yield return new WaitForSeconds(Random.Range(5f,8f)); //Time until the customer has decided what they want
        
        //Show the correct sprite over the customers head
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

        //Create the order
        myOrder = new Order(foodToOrder, GetComponent<Customer>(), spriteRenderer.sprite, selectedDish);
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
            if (textIndex < menuItemTextList.Count)
            {
                menuItemTextList[textIndex].text = food.FoodName;
            }
            textIndex++;
        }
        
        //todo handle player input when choosing which dish we think the alien can eat
        //Currently the first dish is always selected
        selectedDish = foodToChooseFrom[0];

        //Todo line 115 needs to be removed and should be handled down here in the coroutine. Line 64 of "TakeOrderFromCustomer.cs" needs to be called instead of just running directly
        //StartCoroutine(PickDishFromTodaysMenu(foodToChooseFrom));
    }

    IEnumerator PickDishFromTodaysMenu(List<ScriptableFood> foodToChooseFrom)
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && menuItemTextList.Count <= 2)
            {
                selectedDish = foodToChooseFrom[0];
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && menuItemTextList.Count <= 2)
            {
                selectedDish = foodToChooseFrom[1];
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && menuItemTextList.Count <= 3)
            {
                selectedDish = foodToChooseFrom[2];
                break;
            }

            yield return null;
        }

        ToggleSelectableFoodItems();
        
        //Create the order
        myOrder = new Order(foodToOrder, GetComponent<Customer>(), spriteRenderer.sprite, selectedDish);
        readyToOrder = true;
    }

}