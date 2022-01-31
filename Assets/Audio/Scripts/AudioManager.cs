using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //[SerializeField] private AudioSource musicSource;
    [SerializeField] private  AudioSource orderStartSource;
    [SerializeField] private  AudioSource orderCompleteSource;
    [SerializeField] private  AudioSource journalOpenSource;
    [SerializeField] private  AudioSource journalCloseSource;
    [SerializeField] private  AudioSource journalPageTurnSource;
    [SerializeField] private  AudioSource journalPageTurn1Source;
    [SerializeField] private  AudioSource getMoneySource;
    [SerializeField] private  AudioSource reputationUpSource;
    [SerializeField] private  AudioSource reputationDownSource;
    [SerializeField] private AudioSource alienExplodeSource;
    [SerializeField] private AudioSource pickupPlateSource;
    [SerializeField] private AudioSource dayEnd5SecSource;
    [SerializeField] private AudioSource orderScreenOnSource;
    [SerializeField] private AudioSource orderScreenOffSource;
    [SerializeField] private AudioSource alienBigManTalk;
    [SerializeField] private AudioSource alienBigManTalk1;
    [SerializeField] private AudioSource alienBigManTalk2;
    [SerializeField] private AudioSource alienHairyThingTalk;
    [SerializeField] private AudioSource alienHairyThingTalk1;
    [SerializeField] private AudioSource alienHairyThingTalk2;
    [SerializeField] private AudioSource alienHairyThingTalk3;
    [SerializeField] private AudioSource alienSquidThingTalk;
    [SerializeField] private AudioSource alienSquidThingTalk1;

    [Header("Volume Control")]
    [Range(0, 1)]public float volume;

    private void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnValidate()
    {
        foreach(AudioSource audioSource in GetComponentsInChildren<AudioSource>())
        {
            if (audioSource.gameObject.name == "Music")
            {
                audioSource.volume = 0.03f;
            }
            else
            {
                audioSource.volume = volume;
            }
        }
    }
    /*public void PlayMusic()
    {
        if(!musicSource.isPlaying)
        {
            PlaySound(musicSource, musicSource.clip);
        }
        else
        {
            musicSource.Stop();
        }
    }*/

    public void PlayOrderStartSFX()
    {
        PlaySound(orderStartSource, orderStartSource.clip);
    }
    public void PlayOrderCompleteSFX()
    {
        PlaySound(orderCompleteSource, orderCompleteSource.clip);
    }
    public void PlayJournalOpenSFX()
    {
        PlaySound(journalOpenSource, journalOpenSource.clip);
    }
    public void PlayJournalCloseSFX()
    {
        PlaySound(journalCloseSource, journalCloseSource.clip);
    }
    public void PlayJournalPageTurnSFX()
    {
        PlaySound(journalPageTurnSource, journalPageTurnSource.clip);
    }
    public void PlayJournalPageTurn1SFX()
    {
        PlaySound(journalPageTurn1Source, journalPageTurn1Source.clip);
    }
    public void PlayGetMoneySFX()
    {
        PlaySound(getMoneySource, getMoneySource.clip);
    }
    public void PlayReputationUpSFX()
    {
        PlaySound(reputationUpSource, reputationUpSource.clip);
    }
    public void PlayReputationDownSFX()
    {
        PlaySound(reputationDownSource, reputationDownSource.clip);
    }
    public void PlayAlienExplodeSFX()
    {
        PlaySound(alienExplodeSource, alienExplodeSource.clip);
    }
    public void PlayPickupPlateSFX()
    {
        PlaySound(pickupPlateSource, pickupPlateSource.clip);
    }
    public void PlayDayEnd5SecSFX()
    {
        PlaySound(dayEnd5SecSource, dayEnd5SecSource.clip);
    }
    public void PlayOrderScreenOnSFX()
    {
        PlaySound(orderScreenOffSource, orderScreenOffSource.clip);
    }
    public void PlayOrderScreenOffSFX()
    {
        PlaySound(orderScreenOnSource, orderScreenOnSource.clip);
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
