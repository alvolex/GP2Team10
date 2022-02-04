using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [Header("Button References:")] private SerializationManager SerializationManager = new SerializationManager();
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button returnFromCredits;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitGame;

    [Header("Game Objects References:")] [SerializeField]
    private GameObject buttons;

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject optionsMenu;

    [Header("Options References: ")] 
    [SerializeField] private Button resolutionButton;
    [SerializeField] private Button windowModeButton;
    [SerializeField] private Button applySettingsButton;
    [SerializeField] private Button returnFromOptions;
    
    
    private int[] screenResolutionsX = {1920,1600, 1280};
    private int[] screenResolutionsY = {1080,900,720};

    private bool fullscreen = true;
    private int arrayIndex = 0;

    void Start()
    {
        
        resolutionButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Resolution: {screenResolutionsX[0]} : {screenResolutionsY[0]}";
        
        creditsButton.onClick.AddListener(Credits);
        returnFromCredits.onClick.AddListener(ReturnToMenu);
        exitGame.onClick.AddListener(ExitGame);
        optionButton.onClick.AddListener(Options);
        resolutionButton.onClick.AddListener(SetResolution);
        windowModeButton.onClick.AddListener(SetWindowMode);
        applySettingsButton.onClick.AddListener(ApplySetting);
        returnFromOptions.onClick.AddListener(ReturnFromOptions);


        fullscreen = Screen.fullScreen;

        if (Screen.fullScreen)
        {
            windowModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Window Mode: Fullscreen ";
        }
        if (!Screen.fullScreen)
        {
            windowModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Window Mode: Windowed ";
        }
        
    }
    
    public void Credits()
    {
        buttons.SetActive(false);
        credits.SetActive(true);
    }
    public void ReturnToMenu()
    {
        buttons.SetActive(true);
        credits.SetActive(false);
        optionsMenu.SetActive(false);
    }
    public void Options()
    {
        buttons.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ReturnFromOptions()
    {
        
    }

    public void SetResolution()
    {
        arrayIndex++;
        if (arrayIndex+1 > screenResolutionsX.Length)
        {
            arrayIndex = 0;
        }
        resolutionButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Resolution: {screenResolutionsX[arrayIndex]} : {screenResolutionsY[arrayIndex]}";
    }

    public void SetWindowMode()
    {
        
        fullscreen = !fullscreen;

        if (fullscreen)
        {
            windowModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Window Mode: Fullscreen ";
        }

        if (!fullscreen)
        {
            windowModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Window Mode: Windowed ";
        }
        
    }

    public void ApplySetting()
    {
        StartCoroutine(WaitforScreenChange());
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (credits.activeSelf || optionsMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
        if (credits.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
    }

    IEnumerator WaitforScreenChange()
    {
        
        Screen.fullScreen = fullscreen;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        
        Screen.SetResolution(screenResolutionsX[arrayIndex],screenResolutionsY[arrayIndex],Screen.fullScreen);
    }
}
