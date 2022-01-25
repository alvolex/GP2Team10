using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GetAllergySprites
{
    [SerializeField] private Sprite chocolateImage;
    [SerializeField] private Sprite fruitImage;
    [SerializeField] private Sprite proteinImage;
    [SerializeField] private Sprite sauceImage;
    [SerializeField] private Sprite veggiesImage;
    [SerializeField] private Sprite milkproductsImage;

    public Sprite GetSprite(Ingredients.Allergy allergy)
    {
        return allergy switch
        {
            Ingredients.Allergy.Chocolate => chocolateImage,
            Ingredients.Allergy.Fruits => fruitImage,
            Ingredients.Allergy.Protein => proteinImage,
            Ingredients.Allergy.Sauce => sauceImage,
            Ingredients.Allergy.Veggies => veggiesImage,
            Ingredients.Allergy.MilkProduct => milkproductsImage,
            _ => chocolateImage
        };
    }
}