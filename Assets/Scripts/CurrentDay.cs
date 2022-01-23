using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CurrentDay : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private float dayLength = 120f;

    private void Start()
    {
        StartCoroutine(StartDay());
        dayText.text = dayLength.ToString();
    }

    IEnumerator StartDay()
    {
        int i = 0;
        while (i < dayLength)
        {
            yield return new WaitForSeconds(1f);
            i++;

            int timeLeft = (int)dayLength - i;
            
            dayText.text = timeLeft.ToString();
        }
        
        Debug.Log("Day ended!");
    }
}
