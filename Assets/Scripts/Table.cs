using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private List<GameObject> chairPositions;
    private int emptyChairs = 0;
    private int currentChairIndex = 0;
    
    private void Start()
    {
        emptyChairs = chairPositions.Count;
    }

    public Vector3 GetEmptyChairPosition()
    {
        if (emptyChairs != 0)
        {
            Vector3 posToReturn = chairPositions[currentChairIndex].transform.position;
            currentChairIndex++;

            return posToReturn;
        }
        
        return Vector3.positiveInfinity;
    }
}
