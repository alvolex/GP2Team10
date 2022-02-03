using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSunAlongCurve : MonoBehaviour
{
    [SerializeField] private BezierUtility curveT;
    [SerializeField] private GameObject sunObj;

    [SerializeField] private float dayLength = 10f;

    /*[SerializeField] private Material mat1;
    [SerializeField] private Material mat2;
    [SerializeField] private Material mat3;*/

    //[SerializeField] private Image img;

    private float t; // T is a value between 0 and 1
    
    private void Start()
    {
        t = 0f;
    }

    private void Update()
    {
        if (t < 1)
        {
            t = Time.time/dayLength;
            sunObj.transform.position = curveT.QuadraticBezier(t);
        }
    }
}
