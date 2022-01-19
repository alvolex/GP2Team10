using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FoodPickupStation : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private List<Image> spriteRenderers = new List<Image>();
    
    //testing
    private int i = 0;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<Image>().ToList();
    }

    public void UpdateFoodPlatesOnCounter(Sprite img)
    {
        if (i >= spriteRenderers.Count - 1) i = 0;

        spriteRenderers[i].sprite = img;
        i++;
    }
}
