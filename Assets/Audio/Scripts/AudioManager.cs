using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Volume Control")]
    [Range(0, 1)]public float SFXVolume;
    [Range(0, 1)]public float MusicVolume;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource orderStartSource;
    [SerializeField] private AudioSource orderCompleteSource;
    [SerializeField] private AudioSource journalOpenSource;
    [SerializeField] private AudioSource journalCloseSource;
    [SerializeField] private AudioSource journalPageTurnSource;
    [SerializeField] private AudioSource journalPageTurn1Source;
    [SerializeField] private AudioSource getMoneySource;
    [SerializeField] private AudioSource reputationUpSource;
    [SerializeField] private AudioSource reputationDownSource;
    [SerializeField] private AudioSource alienExplodeSource;
    [SerializeField] private AudioSource pickupPlateSource;
    [SerializeField] private AudioSource dayEnd5SecSource;
    [SerializeField] private AudioSource orderScreenOnSource;
    [SerializeField] private AudioSource orderScreenOffSource;
    [SerializeField] private AudioSource alienBigManTalkSource;
    [SerializeField] private AudioSource alienBigManTalk1Source;
    [SerializeField] private AudioSource alienBigManTalk2Source;
    [SerializeField] private AudioSource alienHairyThingTalkSource;
    [SerializeField] private AudioSource alienHairyThingTalk1Source;
    [SerializeField] private AudioSource alienHairyThingTalk2Source;
    [SerializeField] private AudioSource alienHairyThingTalk3Source;
    [SerializeField] private AudioSource alienSquidThingTalkSource;
    [SerializeField] private AudioSource alienSquidThingTalk1Source;
    [SerializeField] private AudioSource garbageCanSource;
    [SerializeField] private AudioSource plateDestroySource;
    [SerializeField] private AudioSource platePunchAwaySource;
    [SerializeField] private AudioSource menuTrackSource;
    [SerializeField] private AudioSource ingameMusicSource;

    

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

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (!menuTrackSource.isPlaying)
            {
                PlayMenuTrack();
            }
            if (ingameMusicSource.isPlaying)
            {
                ingameMusicSource.Stop();
            }
        }
        else
        {
            if (menuTrackSource.isPlaying)
            {
                menuTrackSource.Stop();
            }
            if (!ingameMusicSource.isPlaying)
            {
                PlayIngameMusicTrack();
            }
        }
    }

    private void OnValidate()
    {
        foreach(AudioSource audioSource in GetComponentsInChildren<AudioSource>())
        {
            if (audioSource.gameObject.name == "Music" || audioSource.gameObject.name == "MenuTrack")
            {
                audioSource.volume = MusicVolume;
            }
            else
            {
                audioSource.volume = SFXVolume;
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
    public void PlayAlienBigManTalkSFX()
    {
        int r = Random.Range(1, 4);
        if (r == 1)
        {
            PlaySound(alienBigManTalkSource, alienBigManTalkSource.clip);
        }
        else if (r == 2)
        {
            PlaySound(alienBigManTalk1Source, alienBigManTalk1Source.clip);
        }
        else
        {
            PlaySound(alienBigManTalk2Source, alienBigManTalk2Source.clip);
        }
    }


    public void PlayAlienHairyThingTalkSFX()
    {
        int r = Random.Range(1, 5);
        if (r == 1)
        {
            PlaySound(alienHairyThingTalkSource, alienHairyThingTalkSource.clip);
        }
        else if (r == 2)
        {
            PlaySound(alienHairyThingTalk1Source, alienHairyThingTalk1Source.clip);
        }
        else if (r == 3)
        {
            PlaySound(alienHairyThingTalk2Source, alienHairyThingTalk2Source.clip);
        }
        else
        {
            PlaySound(alienHairyThingTalk3Source, alienHairyThingTalk3Source.clip);
        }
    }

    public void PlayAlienSquidThingTalkSFX()
    {
        int r = Random.Range(1, 3);
        if (r == 1)
        {
            PlaySound(alienSquidThingTalkSource, alienSquidThingTalkSource.clip);
        }
        else
        {
            PlaySound(alienSquidThingTalk1Source, alienSquidThingTalk1Source.clip);
        }
    }
    public void PlayGarbageCanSFX()
    {
        PlaySound(garbageCanSource, garbageCanSource.clip);
    }
    public void PlayPlateDestroySFX()
    {
        PlaySound(plateDestroySource, plateDestroySource.clip);
    }
    public void PlayPlatePunchAwaySFX()
    {
        PlaySound(platePunchAwaySource, platePunchAwaySource.clip);
    }

    public void PlayMenuTrack()
    {
        PlaySound(menuTrackSource, menuTrackSource.clip);
    }
    public void PlayIngameMusicTrack()
    {
        PlaySound(ingameMusicSource, ingameMusicSource.clip);
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
