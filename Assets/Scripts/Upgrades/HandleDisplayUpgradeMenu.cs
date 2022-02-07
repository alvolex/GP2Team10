using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;

public class HandleDisplayUpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeUI;
    
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
