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

    [Header("Tutorial event & text")]
    [SerializeField] private ScriptableTutorialEvent tutorialEvent;
    [SerializeField] private ScriptableTutorialText startText;
    [SerializeField] private ScriptableTutorialText howToSeatCustomers;


    private Queue<string> textPromptsInOrder = new Queue<string>();
    private bool allTextVisible = false;
    private bool shouldShowNextPrompt = true;
    private float timeBetweenCharactersAtStart;

    private bool isInTutorial = false;
    private int tutorialsInQueue = 0;
    private int spotlightIndex = 0;

    private bool checkingForInput = false;

    public ScriptableGameState GameState
    {
        get => gameState;
        set => gameState = value;
    }

    #region Singleton for testing, probably better to just use the showNextPrompt event
    
    public static Tutorial instance;
    private void Awake()
    {
        tutorialEvent.ScriptableEvent += HandleTutorialEvent;
        
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
        gameState.ResetTutorial(); //Todo this needs to be called from a Tutorial start button
        
        //AddStringToList();
        
        //Show first text prompt at startup
        textPromptsInOrder.Enqueue(startText.TutorialText);
        textPromptsInOrder.Enqueue(howToSeatCustomers.TutorialText);
        startText.hasBeenPlayed = true;
        howToSeatCustomers.hasBeenPlayed = true;
        ShowTutorialText();
    }
    
    private void HandleTutorialEvent(ScriptableTutorialText scriptableTutorial)
    {
        if (scriptableTutorial.hasBeenPlayed) return;
        scriptableTutorial.hasBeenPlayed = true;
        
        textPromptsInOrder.Enqueue(scriptableTutorial.TutorialText);
        ShowTutorialText();
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

    public void ShowTutorialText()
    {
        if (!gameState.shouldShowTutorial)return;
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
        
        //StopAllCoroutines(); //todo is this breaking shit?
        
        StartCoroutine(TypeInTextCoroutine());
        tutorialsInQueue--;

        if (tutorialsInQueue > 0)
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(WaitUntilLastTutorialIsFinished());
        }
    }

    IEnumerator TypeInTextCoroutine()
    {
        canvasToToggle.SetActive(true);
        isInTutorial = true;
        //StopCoroutine(CheckForPlayerInput()); //Stop it if it's already running
        if (!checkingForInput)
        {
            StartCoroutine(CheckForPlayerInput());
        }

        currenText.text = "";

        if (textPromptsInOrder.Count == 0) yield break;

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

        TMP_TextInfo tmpLineInfos;
        try
        {
            tmpLineInfos = currenTextHidden.GetTextInfo(currenTextHidden.text)/*?.lineInfo*/;
        }
        catch (Exception e)
        {
            Debug.Log(currenTextHidden.text + " <--- Getting lineinfo from this caused an error");
            Console.WriteLine(e);
            yield break;
        }
        
        
        if (tmpLineInfos != null)
            foreach (var line in tmpLineInfos.lineInfo)
            {
                //Fixes strange behaviour where firstcharindex sometimes gets a wrong(?) value.
                if (line.firstCharacterIndex > currenTextHidden.text.Length)
                {
                    continue;
                }

                foreach (var c in currenTextHidden.text.Substring(line.firstCharacterIndex, line.characterCount))
                {
                    if (!isInTutorial) yield break;

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
        checkingForInput = true;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E) && !allTextVisible)
            {
                allTextVisible = true; //If the player spams space the prompt will close
                timeBetweenCharacters = 0.0001f;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (shouldShowNextPrompt)
                {
                    //This code will only run the first time
                    StopCoroutine(TypeInTextCoroutine());
                    shouldShowNextPrompt = false;
                    isInTutorial = false;
                    checkingForInput = false;
                    
                    yield return new WaitForSeconds(0.1f);

                    ShowTutorialText();
                    yield break;
                }
                
                isInTutorial = false;
                canvasToToggle.SetActive(false);
                checkingForInput = false;
                StopCoroutine(TypeInTextCoroutine());
                yield break;
            }

            yield return null;
        }
    }
}
