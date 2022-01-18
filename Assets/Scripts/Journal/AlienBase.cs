using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlienBase : MonoBehaviour
{
    [SerializeField] private List<string> Allergies;
    [SerializeField] private List<string> alienContext;
    [SerializeField] private List<string> alienType;

    [SerializeField] private Color textHighlightColor;
    
    [SerializeField] private TextMeshProUGUI journalText;
    
    [SerializeField] private Text Diet;
    [SerializeField] private Text Type;





    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        /*journalText.text =
            "Karox Langs\n" +
            "Andromeda Galaxy \n\n" +
            "The Karox Species are nativve to the " +
            "bongoschpongo gas giant in the karox system, Their histroy has always been troubled with war. " +
            "For thousands of yearsthey have been fighting with the Quolasas.\n\n" +
            "Diet They are omnivores that " +
            "enjoy all sorts of food. They have only been to earth for 10 years though and are cuatios with  " +
            "what they can eat here . Their stomachs cannot consume fat proteins that come in meat. " +
            "Their stomach acid reacts violenty and turns solid turning their entire body to stone.\n\n" +
            "THe karox species come in many different races, some have blue ear tips. " +
            $"<mark=#{ColorUtility.ToHtmlStringRGBA(textHighlightColor)}><b>" +
            "They do have the capacity to consume meat " +
            "but are mildly allergic to earth plants. They cannot eatr sallads or any greens at al </mark></b>. " +
            "They get stomach aches and rashes. They have been recorded to throw up after eating greens in some cases";
        */
        
    }
}
