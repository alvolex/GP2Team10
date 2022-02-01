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
    [SerializeField] private TutorialTextPrompts tutText;

    [Header("Lights")] 
    [SerializeField] private GameObject spotLightTutorial;
    [SerializeField] private List<Transform> spotlightPositions = new List<Transform>();

    [Header("Event")]
    [SerializeField] private ScriptableSimpleEvent showNextPrompt;


    private Queue<string> textPromptsInOrder = new Queue<string>();
    private bool allTextVisible = false;
    private bool shouldShowNextPrompt = true;
    private float timeBetweenCharactersAtStart;

    private bool isInTutorial = false;
    private int tutorialsInQueue = 0;

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
        textPromptsInOrder.Enqueue(tutText.startString);
        textPromptsInOrder.Enqueue(tutText.howToSeatCustomers);
        textPromptsInOrder.Enqueue(tutText.alienSeatedString);
        textPromptsInOrder.Enqueue(tutText.alienTakeOrderString);
        textPromptsInOrder.Enqueue(tutText.orderTakenString);
        textPromptsInOrder.Enqueue(tutText.orderLeftAtKitchen);
        textPromptsInOrder.Enqueue(tutText.foodAtTheCounterString);
        textPromptsInOrder.Enqueue(tutText.alienRecievedFoodString);
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

        if (!isInTutorial)
        {
            StartCoroutine(TypeInTextCoroutine());
        }
        else
        {
            tutorialsInQueue++;
            StartCoroutine(WaitUntilLastTutorialIsFinished());
        }
    }

    public void TurnOnSeatedCustomerSpotlight()
    {
        spotLightTutorial.SetActive(true);
    }

    IEnumerator WaitUntilLastTutorialIsFinished()
    {
        while (isInTutorial)
        {
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(TypeInTextCoroutine());
        tutorialsInQueue--;

        if (tutorialsInQueue != 0)
        {
            StartCoroutine(WaitUntilLastTutorialIsFinished());
        }
    }

    IEnumerator TypeInTextCoroutine()
    {
        isInTutorial = true;
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
        isInTutorial = false;
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
