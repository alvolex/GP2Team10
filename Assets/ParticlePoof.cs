using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoof : MonoBehaviour
{
    private ParticleSystem poofer;
    
    private void Start()
    {
        poofer = GetComponent<ParticleSystem>();
    }

    public void AlienGoPoo0f(Transform alienPos)
    {
        poofer.transform.position = alienPos.position;
        poofer.Play();
    }
}

