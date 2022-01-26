using System;
using System.Collections;
using System.Collections.Generic;
using Hazard;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float hazardSpawnRadius;
    [SerializeField, Tooltip("This is the middle point of the circle")] private Transform hazardOrigin;

    [Header("Vars")]
    private List<Hazards> allHazards = new List<Hazards>();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hazardOrigin.position, hazardSpawnRadius);
    }
}

