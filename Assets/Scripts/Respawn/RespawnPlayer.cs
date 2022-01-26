using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Respawning Player");
        if (other.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}
