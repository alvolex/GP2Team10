using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeToAndFrom : MonoBehaviour
{
    private CanvasGroup blackImage;

    [SerializeField] private float fadeSpeed;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        blackImage = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        blackImage.alpha = Mathf.Lerp(blackImage.alpha, 0, fadeSpeed);
    }
}
