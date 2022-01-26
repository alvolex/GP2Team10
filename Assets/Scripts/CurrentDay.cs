using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CurrentDay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private float dayLength = 120f;
    
    [Header("Daylight")] 
    [SerializeField] private Light sun;

    private Quaternion sunStartRotation;
    private int currentDay = 1;
    
    public event Action OnDayEnded;

    private void Start()
    {
        dayText.text = $"Day: {currentDay} | {dayLength.ToString()}";
        sunStartRotation = sun.transform.rotation;
        StartCoroutine(StartDay());

        OnDayEnded += delegate { StartCoroutine(StartDay()); }; //Test
    }

    private void Update()
    {
        RotateSun();
    }

    private void RotateSun()
    {
        sun.transform.Rotate(Vector3.right * 200f / dayLength * Time.deltaTime); //Slightly more than a half a rotation per day cycle
    }

    IEnumerator StartDay()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeSun(0,1, 0.3f));
        
        int i = 0;
        sun.transform.rotation = sunStartRotation;
        while (i < dayLength)
        {
            yield return new WaitForSeconds(1f);
            i++;

            int timeLeft = (int)dayLength - i;
            string uiText = $"Day: {currentDay} | {timeLeft.ToString()}";
            dayText.text = uiText;

            if (timeLeft == 5)
            {
                AudioManager.Instance.PlayDayEnd5SecSFX();
            }            
        }
        currentDay++;
        
        OnDayEnded?.Invoke(); //Event that will be triggered when a day has ended
        Debug.Log("Day ended!");

        StartCoroutine(FadeSun(1,0, 0.4f));
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
