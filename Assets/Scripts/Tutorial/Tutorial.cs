using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Vars")]
    [SerializeField] private float timeBetweenCharacters = 0.02f;

    [Header("References")]
    [SerializeField] private TMP_Text currenText;
    [SerializeField] private TMP_Text currenTextHidden;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private GameObject canvasToToggle;

    [SerializeField] private ScriptableGameState gameState;

    [Header("Event")]
    [SerializeField] private ScriptableSimpleEvent showNextPrompt;

    #region Strings
    private string startString =
        "On this fine day your duty is to serve as many of your alien brethren as possible. Travelers from all over the galaxy converge on this fine establishment to purchase some of the most sought after meals in the universe. +When a patron arrives, help them find an vacant table.";

    private string howToSeatCustomers = "To help patrons find their way to a table, simply walk up to them and interact with their group (Spacebar), then find a suitable table and interact with it as well! (Spacebar)";
    
    private string alienSeatedString = "Well done, friend! You've successfully seated your first patrons, it's quite likely that they will, at some point, want to order some food. When that happens, take their order and try not to butcher them. Aliens are quite sensitive when it comes to their gastronomical needs.";
    
    private string alienTakeOrderString = "I'll be damned, this is one hungry fella. Some dietary needs are quite hard to satisfy, but seeing as we're the finest eatery in the galaxy we strive to always be prepared! Be careful that you don't choose a meal that the alien is allergic to, they will tell you what they can't eat when you take their order. +(Use 1,2,3 on your keyboard to select a dish from the list.)";

    private string orderTakenString = "Hopefully we managed to satisfy their dietary needs, we really couldn't afford another lawsuit.. Now let's scurry over to the kitchen and hand them the order, the quicker the better so that the chef's don't get overwhelmed!";

    private string orderLeftAtKitchen =
        "Fantastic work! Now the chef is cooking that delicious grub. Once the food is ready it will be delivered to the counter!";

    private string foodAtTheCounterString = "Although the chefs can be quite salty at times, their cooking abilities are top-notch when it comes to intergalactic dining. Be sure to deliver the order to the correct customer, post-haste!";

    private string alienRecievedFoodString = "Wow, you made it, my bet was that you'd quit before you managed to deliver your first order. Stay vigilant, some of these aliens will really test your patience!";
    #endregion
    
    private Queue<string> textPromptsInOrder = new Queue<string>();
    private bool allTextVisible = false;
    private bool shouldShowNextPrompt = true;
    private float timeBetweenCharactersAtStart;

    public ScriptableGameState GameState
    {
        get => gameState;
        set => gameState = value;
    }

    #region Singleton for testing, probably better to just use the showNextPrompt event
    
    public static Tutorial instance;
    private void Awake()
    {
        if (ReferenceEquals(instance, null))
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    

    private void Start()
    {
        if (!gameState.shouldShowTutorial) return;
        timeBetweenCharactersAtStart = timeBetweenCharacters;
        
        AddStringToList();
        ShowTutorialText(true);
        //showNextPrompt.ScriptableEvent += ShowTutorialText;
    }

    /*private void OnDestroy()
    {
        showNextPrompt.ScriptableEvent -= ShowTutorialText;
    }*/

    private void AddStringToList()
    {
        textPromptsInOrder.Enqueue(startString);
        textPromptsInOrder.Enqueue(howToSeatCustomers);
        textPromptsInOrder.Enqueue(alienSeatedString);
        textPromptsInOrder.Enqueue(alienTakeOrderString);
        textPromptsInOrder.Enqueue(orderTakenString);
        textPromptsInOrder.Enqueue(orderLeftAtKitchen);
        textPromptsInOrder.Enqueue(foodAtTheCounterString);
        textPromptsInOrder.Enqueue(alienRecievedFoodString);
    }

    public void ShowTutorialText(bool shouldShowTutorial)
    {
        if (!shouldShowTutorial)return;
        //Reset the values
        timeBetweenCharacters = timeBetweenCharactersAtStart;
        allTextVisible = false;

        //Turn off the tutorial if the player has finished it
        if (textPromptsInOrder.Count == 1)
        {
            gameState.shouldShowTutorial = false;
        }
        
        canvasToToggle.SetActive(true);
        StartCoroutine(TypeInTextCoroutine());
    }

    IEnumerator TypeInTextCoroutine()
    {
        StopCoroutine(CheckForPlayerInput()); //Stop it if it's already running
        StartCoroutine(CheckForPlayerInput());
        
        currenText.text = "";
        currenTextHidden.text = textPromptsInOrder.Dequeue();

        bool containsLinebreak = currenTextHidden.text.Contains("+");
        string lineBreakString = "";
        
        //Handle text that contains an extra linebreak ( '+' sign in the string)
        if (containsLinebreak)
        {
            lineBreakString = currenTextHidden.text.Substring(currenTextHidden.text.LastIndexOf('+') + 1);
            currenTextHidden.text = currenTextHidden.text.Split('+')[0];
        }

        //Get all the text that will fit on one line, then write out each char separately
        foreach (var line in currenTextHidden.GetTextInfo(currenTextHidden.text).lineInfo)
        {
            //Fixes strange behaviour where firstcharindex sometimes gets a wrong(?) value.
            if (line.firstCharacterIndex > currenTextHidden.text.Length)
            {
                continue;
            }
           
            foreach (var c in currenTextHidden.text.Substring(line.firstCharacterIndex, line.characterCount))
            {
                yield return new WaitForSeconds(timeBetweenCharacters);
                currenText.text += $"{c}";
            }
            currenText.text += '\n';
        }

        if (containsLinebreak)
        {
            foreach (var c in lineBreakString)
            {
                yield return new WaitForSeconds(timeBetweenCharacters);
                currenText.text += $"{c}";
            }
        }

        allTextVisible = true;
        continuePanel.SetActive(true);
    }

    IEnumerator CheckForPlayerInput()
    {
        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space) && !allTextVisible)
            {
                timeBetweenCharacters = 0.0001f;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (shouldShowNextPrompt)
                {
                    ShowTutorialText(true);
                    shouldShowNextPrompt = false;
                    yield break;
                }
                
                canvasToToggle.SetActive(false);
                yield break;
            }
        }
    }
}
