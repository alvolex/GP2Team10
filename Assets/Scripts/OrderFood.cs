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
    [SerializeField] private ScriptablePlayerCurrentAction curAction;

    [SerializeField] private GameObject orderUIImage;
    [SerializeField] private GameObject chooseMenuItemImage;
    [SerializeField] private ScriptableTodaysMeals todaysMeals;

    [Header("Food Ordering")] 
    [SerializeField, Range(1f, 3f)] private float foodOrderingRadius = 2f;

    [Header("Sprites for the different food types")] [SerializeField]
    private Image spriteRenderer;

    [SerializeField] private Sprite starterSprite;
    [SerializeField] private Sprite maincourseSprite;
    [SerializeField] private Sprite dessertSprite;

    [Header("Allergy sprites")] 
    [SerializeField] private List<Image> allergySpriteRenderer = new List<Image>();
    [SerializeField] private ScriptableAllergySpriteHandler getSprite;

    //All private vars and getters
    #region Private vars and getters
    private List<Ingredients.Allergy> myAllergies;

    private SphereCollider sCollider;
    private bool readyToOrder;
    private bool hasOrdered;
    private Array enumArr;
    private ScriptableFood selectedDish;
    private FoodType foodToOrder;
    private TakeOrderFromCustomer playerRef;
    private List<ScriptableFood> foodToChooseFrom;

    private List<TMP_Text> menuItemTextList = new List<TMP_Text>();

    //Can be used to check if the ordered food matches what they received
    private Order myOrder;
    public Order MyOrder => myOrder;

    public TakeOrderFromCustomer PlayerRef
    {
        set => playerRef = value;
    }

    public bool HasOrdered
    {
        get => hasOrdered;
        set => hasOrdered = value;
    }
    #endregion

    private void Start()
    {
        EnumToArray(); //Convert our FoodTypes into an array
        menuItemTextList = chooseMenuItemImage.GetComponentsInChildren<TMP_Text>().ToList();
        hasOrdered = false;
        myAllergies = GetComponent<AlienAttributes>().allergy.ToList();
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

    public void StartOrderProcess()
    {
        OrderUIImage.SetActive(false); //Disable the order food pop-up since we've now taken the order
        ToggleSelectableFoodItems(); //Shows the different items we can choose from
        StartPickFoodCoroutine();
    }

    IEnumerator TimeToOrder()
    {
        foodToOrder = (FoodType) enumArr.GetValue(Random.Range(0, enumArr.Length - 1)); //Get a random food type that the customer wants to order

        //Update the UI that shows the dishes with the correct info
        UpdateMenuItemsTextWithTodaysFood(foodToOrder);

        yield return new WaitForSeconds(Random.Range(5f, 8f)); //Time until the customer has decided what they want

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

        readyToOrder = true; //Flag that the customer is ready to order food

        //Make the collider bigger again when the alien is seated so that we can handle orders
        sCollider.radius = foodOrderingRadius; //todo this value might need to be tweaked further
    }

    private void UpdateMenuItemsTextWithTodaysFood(FoodType foodType)
    {
        foodToChooseFrom = new List<ScriptableFood>();

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
        List<Image> allergyItemList = new List<Image>();
        foreach (var food in foodToChooseFrom)
        {
            if (textIndex < menuItemTextList.Count)
            {
                //Set the food names (Eg. Dessert 1, Dessert 2, Starter 1..)
                menuItemTextList[textIndex].text = food.FoodName;
                allergyItemList = menuItemTextList[textIndex].GetComponentsInChildren<Image>().ToList();
                
                //Update the allergy sprites for the current food
                int allergyIndex = 0;
                foreach (var img in allergyItemList)
                {
                    //Inactivate the allergy image box if there are fewer allergies
                    if (allergyIndex >= food.Allergies.Count)
                    {
                        allergyItemList[allergyIndex].gameObject.SetActive(false);
                        allergyIndex++;
                        continue;
                    }
                    
                    //Set allergy picture
                    img.sprite = getSprite.GetSprite(food.Allergies[allergyIndex]);
                    allergyIndex++;
                }
            }
            textIndex++;
        }
        
        //Update current aliens allergy sprites
        int spriteIndex = 0;
        foreach (var allergy in myAllergies)
        {
            Sprite allergySprite = getSprite.GetSprite(allergy);
            allergySpriteRenderer[spriteIndex].sprite = allergySprite;
            allergySpriteRenderer[spriteIndex].gameObject.SetActive(true);
            spriteIndex++;
        }
        

        selectedDish = foodToChooseFrom[0]; //Just a failsafe
    }

    public void StartPickFoodCoroutine()
    {
        //todo find a better way to start this co-routine
        StartCoroutine(PickDishFromTodaysMenu(foodToChooseFrom));
    }

    IEnumerator PickDishFromTodaysMenu(List<ScriptableFood> foodToChooseFrom)
    {
        CurrentAction prevAction = curAction.CurrentAction;
        curAction.CurrentAction = CurrentAction.TakingOrder;

        AudioManager.Instance.PlayOrderScreenOnSFX();

        if (Tutorial.instance != null)
        {
            Tutorial.instance.ShowTutorialText(Tutorial.instance.GameState.howToTakeOrderTutorial);
            if (Tutorial.instance.GameState.howToTakeOrderTutorial)
            {
                Tutorial.instance.TurnOnAndMoveSpotlight();
            }
            Tutorial.instance.GameState.howToTakeOrderTutorial = false;
        }
        

        while (true)
        {
            if (chooseMenuItemImage.activeSelf == true && Vector3.Distance(transform.position, playerRef.transform.position) < 3.5f)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && menuItemTextList.Count >= 1)
                {
                    selectedDish = foodToChooseFrom[0];
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && menuItemTextList.Count >= 2)
                {
                    selectedDish = foodToChooseFrom[1];
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) && menuItemTextList.Count >= 3)
                {
                    selectedDish = foodToChooseFrom[2];
                    break;
                }
            }
            else
            {
                curAction.CurrentAction = prevAction;
                orderUIImage.SetActive(true); //Re-activate the image that shows that they want to order a meal
                ToggleSelectableFoodItems();
                AudioManager.Instance.PlayOrderScreenOffSFX();
                yield break;
            }
            
            yield return null;
        }

        ToggleSelectableFoodItems();
        
        //Show tutorial text after taking order
        if (Tutorial.instance != null)
        {
            Tutorial.instance.ShowTutorialText(Tutorial.instance.GameState.hasTakenOrderTutorial);
            if (Tutorial.instance.GameState.hasTakenOrderTutorial)
            {
                Tutorial.instance.TurnOnAndMoveSpotlight();
            }
            Tutorial.instance.GameState.hasTakenOrderTutorial = false;
        }
        
        //Create the order
        myOrder = new Order(foodToOrder, GetComponent<Customer>(), spriteRenderer.sprite, selectedDish);

        curAction.CurrentAction = CurrentAction.HandlingOrder;
        HasOrdered = true;

        AudioManager.Instance.PlayOrderScreenOffSFX();

        //Call QueueUpOrder
        playerRef.QueueUpOrder(myOrder); //Hand the order to the player
        curAction.CurrentAction = CurrentAction.HandlingOrder;
    }
}