using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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

    [Header("Food Ordering")] 
    [SerializeField, Range(1f,3f)] private float foodOrderingRadius = 2f;

    //todo TESTING, THIS STUFFNEEDS TO BE MOVED INTO ITS OWN FOOD CLASS WITH ATTRIBUTES AND ALLERGIES AND WHATNOT
    [Header("This will all be moved later..")] 
    [SerializeField] private Image spriteRenderer;
    [SerializeField] private Sprite starterSprite;
    [SerializeField] private Sprite maincourseSprite;
    [SerializeField] private Sprite dessertSprite;

    private SphereCollider sCollider;
    private bool readyToOrder;
    private Array enumArr;

    //Can be used to check if the ordered food matches what they received
    private Order myOrder;
    public Order MyOrder => myOrder;

    private void Start()
    {
        EnumToArray(); //Convert our FoodTypes into an array
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

    public void Order()
    {
        StartCoroutine(TimeToOrder());
    }

    IEnumerator TimeToOrder()
    {
        //Move to other class..
        FoodType foodToOrder = (FoodType) enumArr.GetValue(Random.Range(0, enumArr.Length - 1)); //Get a random food (remove last since that is "Not ordered"..)

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

        //Uncomment these two if we want to go back to how it was before we showed food sprite
        //yield return new WaitForSeconds(Random.Range(5f,8f));
        //orderUIImage.SetActive(true);
        
        readyToOrder = true;
        //Make the collider bigger again when the alien is seated so that we can handle orders
        sCollider.radius = foodOrderingRadius; //todo this needs tweaking
    }
}