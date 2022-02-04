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
    private int spotlightIndex = 0;

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
        gameState.ResetAll();
        
        AddStringToList();
        ShowTutorialText(true);
    }

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
        if (!shouldShowTutorial || !gameState.shouldShowTutorial)return;
        //Reset the values
        timeBetweenCharacters = timeBetweenCharactersAtStart;
        allTextVisible = false;

        //Turn off the tutorial if the player has finished it
        if (textPromptsInOrder.Count == 0)
        {
            gameState.shouldShowTutorial = false;
            return;
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

    public void TurnOnAndMoveSpotlight()
    {
        //todo read the position list and lerp (move towards?)
        if (spotlightPositions.Count <= spotlightIndex) 
        {
            Debug.Log("Position missing, can't move spotlight");
            return;
        }
        
        spotLightTutorial.SetActive(true);
        spotlightIndex++;
        spotLightTutorial.transform.position = spotlightPositions[spotlightIndex].transform.position;
    }

    IEnumerator WaitUntilLastTutorialIsFinished()
    {
        while (isInTutorial)
        {
            yield return new WaitForSeconds(0.2f);
        }

        allTextVisible = false;
        
        StopAllCoroutines(); //todo is this breaking shit?

        StartCoroutine(TypeInTextCoroutine());
        tutorialsInQueue--;

        if (tutorialsInQueue > 0)
        {
            StartCoroutine(WaitUntilLastTutorialIsFinished());
        }
    }

    IEnumerator TypeInTextCoroutine()
    {
        isInTutorial = true;
        //StopCoroutine(CheckForPlayerInput()); //Stop it if it's already running
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
            /*if (!isInTutorial)
            {
                yield break;
            }*/

            //Fixes strange behaviour where firstcharindex sometimes gets a wrong(?) value.
            if (line.firstCharacterIndex > currenTextHidden.text.Length)
            {
                Debug.Log("Continue");
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
                if (!isInTutorial)
                {
                    yield break;
                }
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

            if (Input.GetKeyDown(KeyCode.E) && !allTextVisible)
            {
                allTextVisible = true; //If the player spams space the prompt will close
                timeBetweenCharacters = 0.0001f;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (shouldShowNextPrompt)
                {
                    ShowTutorialText(true);
                    shouldShowNextPrompt = false;
                    isInTutorial = false;
                    yield break;
                }
                
                isInTutorial = false;
                canvasToToggle.SetActive(false);
                yield break;
            }
        }
    }
}
