using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private  AudioSource orderStartSource;
    [SerializeField] private  AudioSource orderCompleteSource;
    [SerializeField] private  AudioSource journalOpenSource;
    [SerializeField] private  AudioSource journalCloseSource;
    [SerializeField] private  AudioSource journalPageTurnSource;
    [SerializeField] private  AudioSource journalPageTurn1Source;
    [SerializeField] private  AudioSource getMoneySource;
    [SerializeField] private  AudioSource reputationUpSource;
    [SerializeField] private  AudioSource reputationDownSource;

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
    public void PlayMusic()
    {
        if(!musicSource.isPlaying)
        {
            PlaySound(musicSource, musicSource.clip);
        }
        else
        {
            musicSource.Stop();
        }
    }

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

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
