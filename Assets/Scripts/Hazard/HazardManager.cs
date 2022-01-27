using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HazardManager : MonoBehaviour
{
    [Header("Hazards")]
    [SerializeField] private Hazard slowDownPuddle;

    [Header("Setup")] 
    [SerializeField] private float minTimeBetween = 10;
    [SerializeField] private float maxTimeBetween = 30;

    [SerializeField] private float hazardSpawnRadius;
    [SerializeField, Tooltip("This is the middle point of the circle")] private Transform hazardOrigin;

    [Header("Vars")]
    private List<Hazard> allHazards = new List<Hazard>();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hazardOrigin.position, hazardSpawnRadius);
    }

    private void Start()
    {
        StartCoroutine(HazardSpawner());
    }

    IEnumerator HazardSpawner()
    {
        while (true)
        {
            float timeToNextSpawn = Random.Range(minTimeBetween, maxTimeBetween);
            yield return new WaitForSeconds(timeToNextSpawn);

            Vector3 pos = Random.insideUnitSphere* hazardSpawnRadius;
            Vector3 v3Pos = new Vector3(pos.x, hazardOrigin.transform.position.y, pos.z);

            Instantiate(slowDownPuddle.gameObject, v3Pos, Quaternion.identity);

        }
    }
}

