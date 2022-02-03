using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentDay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float dayLength = 120f;

    [SerializeField, Tooltip("When the time left of the day is below this value we will stop spawning more customers")] 
    private int timeLeftToStopSpawning;

    [Header("UI")]
    [SerializeField] private TMP_Text dayText;

    [Header("Daylight")] 
    [SerializeField] private Light sun;
    [SerializeField] private float skyboxRotSpeed;

    [Header("Event")] 
    [SerializeField] private ScriptableSimpleEvent dayEndEvent;
    [SerializeField] private ScriptableSimpleEvent startNextDay;
    [SerializeField] private ScriptableSimpleEvent stopSpawningCustomers;

    private Quaternion sunStartRotation;
    private int currentDay = 1;

    private void Start()
    {
        sunStartRotation = sun.transform.rotation;
        StartCoroutine(StartDay());

        startNextDay.ScriptableEvent += delegate { StartCoroutine(StartDay()); };
        RenderSettings.skybox.SetFloat("_Rotation", 0);
    }

    private void OnDestroy()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 0);
    }

    private void Update()
    {
        RotateSun();
        RotateSkybox();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void RotateSkybox()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotSpeed);
    }

    private void RotateSun()
    {
        sun.transform.Rotate(Vector3.right * 200f / dayLength * Time.deltaTime); //Slightly more than a half a rotation per day cycle
    }

    IEnumerator StartDay()
    {
        SetMinutesAndSecondsLeft(0);
        
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeSun(0,1, 0.3f));

        int i = 0;
        sun.transform.rotation = sunStartRotation;
        while (i < dayLength)
        {
            i++;
            var timeLeft = SetMinutesAndSecondsLeft(i);

            if (timeLeft == timeLeftToStopSpawning)
            {
                Debug.Log("Invoking stop spawning");
                stopSpawningCustomers.InvokeEvent();
            }

            if (timeLeft == 5)
            {
                AudioManager.Instance.PlayDayEnd5SecSFX();
            }
            yield return new WaitForSeconds(1f);
        }
        currentDay++;
        
        dayEndEvent.InvokeEvent();
        Debug.Log("Day ended!");

        StartCoroutine(FadeSun(1,0, 0.4f));
    }

    private int SetMinutesAndSecondsLeft(int i)
    {
        int timeLeft = (int) dayLength - i;

        int minutes = timeLeft / 60;
        int seconds = timeLeft % 60;

        string minAndSecs = $"{(minutes > 0 ? $"{minutes}:" : "")}{seconds}";
        string uiText = $"Day: {currentDay} | {minAndSecs}";
        dayText.text = uiText;
        return timeLeft;
    }

    IEnumerator FadeSun(int start, int end, float time)
    {
        int startVal = start;  
        int endVal = end;

        float timeToFade = time;  
        
        float intensity = 0;
        
        for(float f = 0; f <= timeToFade; f += Time.deltaTime) {
            intensity = Mathf.Lerp(startVal, endVal, f / timeToFade); // passing in the start + end values, and using our elapsed time 'f' as a portion of the total time 'x'

            sun.intensity = intensity;
            
            yield return null;
        }
    }
}
