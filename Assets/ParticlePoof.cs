using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ParticlePoof : MonoBehaviour
{
    private ParticleSystem poofer;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private float timeToShakeCam = 0.3f;

    
    
    
    private void Start()
    {
        poofer = GetComponent<ParticleSystem>();
    }

    public void AlienGoPoo0f(Customer alienPos)
    {
        //poofer.transform.position = new Vector3(alienPos.transform.position.x-0.4f,alienPos.transform.position.y,alienPos.transform.position.z);
        
        
        poofer.transform.position = new Vector3(alienPos.ChairPos.x,alienPos.ChairPos.y+1.5f,alienPos.ChairPos.z);
        
        
        poofer.Play();
        StartCoroutine(CameraShake());
    }
    
    

    IEnumerator CameraShake()
    {
        var camComponent = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        camComponent.m_AmplitudeGain = 1;
        camComponent.m_FrequencyGain = 1;
        yield return new WaitForSeconds(timeToShakeCam);
        camComponent.m_AmplitudeGain = 0;
        camComponent.m_FrequencyGain = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            poofer.Play();
            StartCoroutine(CameraShake());
        }
    }
}

