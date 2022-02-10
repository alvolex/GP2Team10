using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleDisplayUpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject tutorialFinishedUI;
    
    
    [Header("Events")]
    [SerializeField] private ScriptableSimpleEvent dayEnd;
    [SerializeField] private ScriptableSimpleEvent startNextDay;
    private FadeToAndFrom fadeToAndFrom;
    
    private bool showUI = false;

    private void Start()
    {
        dayEnd.ScriptableEvent += StartFadeIntoEvent;
        fadeToAndFrom = FindObjectOfType<FadeToAndFrom>();
    }

    private void OnDestroy()
    {
        dayEnd.ScriptableEvent -= StartFadeIntoEvent;
    }

    void StartFadeIntoEvent()
    {
        StartCoroutine(FadeInto());
    }
    public void StartFadeFromEvent()
    {
        StartCoroutine(FadeFrom());
    }

    void ToggleUI()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MAIN_Tutorial")) //Todo not great that this is set by name
        {
            tutorialFinishedUI.SetActive(!upgradeUI.activeSelf);
            Time.timeScale = 0;
            return;
        }
        
        upgradeUI.SetActive(!upgradeUI.activeSelf);
        Time.timeScale = 0;
    }

    public void StartNextDay()
    {
        startNextDay.InvokeEvent();
        Time.timeScale = 1;
    }

    IEnumerator FadeInto()
    {
        while (fadeToAndFrom.blackImage.alpha < 0.999f)
        {
            fadeToAndFrom.blackImage.alpha = Mathf.Lerp( fadeToAndFrom.blackImage.alpha, 1,  fadeToAndFrom.fadeSpeed);
            yield return null;
        }
        ToggleUI();
        yield return null;
    }
    IEnumerator FadeFrom()
    {
        ToggleUI();
        while (fadeToAndFrom.blackImage.alpha > 0.01f)
        {
            fadeToAndFrom.blackImage.alpha = Mathf.Lerp( fadeToAndFrom.blackImage.alpha, 0,  fadeToAndFrom.fadeSpeed);
            yield return null;
        }
        StartNextDay();
        yield return null;
    }
}
