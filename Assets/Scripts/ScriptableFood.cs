using System.Collections.Generic;
using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "new Food", menuName = "SO/New Food", order = 0)]
    public class ScriptableFood : ScriptableObject
    {
        [SerializeField] private string foodName = "Alien Grub";
        [SerializeField] private FoodType foodType = FoodType.NotOrdered;
        [SerializeField] private List<Ingredients.Allergy> allergies = new List<Ingredients.Allergy>();
        [SerializeField, Tooltip("In seconds")] private float timeToCookFood = 5f;
        [SerializeField, Tooltip("The prefab that will be spawned on the kitchen counter & carried to the alien")] private GameObject foodPrefab;
        [SerializeField, TextArea(4,8)] private string foodDescription;
        
        
        
        /* Not sure if this will be needed as the Aliens won't chose EXACTLY which food they want, just if they want a starter, main course or a dessert.
        This is only needed if every single dish will have it's own sprite */
        //[SerializeField, Tooltip("Used in the bubble above the aliens & in the kitchen while being cooked")] private Sprite foodSprite;
        
        /*Getters*/
        public string FoodName => foodName;
        public FoodType FoodType => foodType;
        public List<Ingredients.Allergy> Allergies => allergies;
        public float TimeToCookFood => timeToCookFood;
        public GameObject FoodPrefab => foodPrefab;
        public string FoodDescription => foodDescription;
    }
}