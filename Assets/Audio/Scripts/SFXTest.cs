using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTest : MonoBehaviour
{
    private AudioClip clip;
    private AudioSource source;

    private void Awake()
    {
        
        source = GetComponent<AudioSource>();
        clip = source.clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlaySound();
    }

    private void PlaySound()
    {
        if (!source.isPlaying)
        {
            AudioManager.Instance.PlaySound(source, clip);
        }
    }
}
