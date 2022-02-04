using System.Collections;
using System.Collections.Generic;
using Scriptables;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeToAndFrom : MonoBehaviour
{
    [SerializeField] private ScriptableSimpleEvent dayEnd;
    [HideInInspector]public CanvasGroup blackImage;
    
    public float fadeSpeed;

    [HideInInspector]public bool fadeIntoBlack = true;
    private bool dayEnded;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        blackImage = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (!dayEnded)
        {
            blackImage.alpha = Mathf.Lerp(blackImage.alpha, 0, fadeSpeed);
            
        }
        if (fadeIntoBlack)
        {
            blackImage.alpha = Mathf.Lerp(blackImage.alpha, 1, fadeSpeed);
        }
    }*/
}
