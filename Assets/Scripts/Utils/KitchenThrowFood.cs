using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenThrowFood : MonoBehaviour
{
    [SerializeField] private BezierUtility curveT;
    [SerializeField] private GameObject foodToThrow;
    [SerializeField] private GameObject hazardToSpawn;
    

    [SerializeField] private float airTime = 2f;

    private float t; // T is a value between 0 and 1
    private static readonly int Alpha = Shader.PropertyToID("Alpha");

    private void Start()
    {
        t = 0f;
    }

    public void ThrowFood()
    {
        StartCoroutine(StartThrowFood());
    }

    IEnumerator StartThrowFood()
    {
        AudioManager.Instance.PlayPlatePunchAwaySFX(); Debug.Log("gamer time");
        t = 0;
        float startTime = Time.time;

        GameObject foodInstance =  Instantiate(foodToThrow);

        while (t < 1)
        {
            t = (Time.time - startTime)/airTime;
            foodInstance.transform.position = curveT.QuadraticBezier(t);
            yield return null;
        }

        GameObject hazardInstance =  Instantiate(hazardToSpawn);
        //hazardInstance.GetComponent<Renderer>().sharedMaterial.SetFloat(Alpha, 0.8f);
        hazardInstance.transform.position = foodInstance.transform.position;
        Destroy(foodInstance);
        AudioManager.Instance.PlayPlateDestroySFX(); Debug.Log("yike");
        Destroy(hazardInstance, 7f);
    }

}
