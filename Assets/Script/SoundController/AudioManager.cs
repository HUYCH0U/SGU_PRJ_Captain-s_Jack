using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour   
{
    public static AudioManager Instance;
    public Sound[] MusicSound, SfxSound, FootStepSound;
    public AudioSource MusicSource, SfxSource, FootStepSource;
    public bool IsBackGroundMusicTurnOf = false;
    public int chap;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        chap = 1;
        PlayMusic();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }


    public void PlayMusic()
    {
        if (!MusicSource.isPlaying)
        {
            MusicSource.clip = MusicSound[chap - 1].SoundClip;
            MusicSource.Play();
        }
    }
    public void PlayFootStep()
    {
        int min = 0, max = 0;
        switch (chap)
        {
            case 1:
                min = 1;
                max = 4;
                break;
            case 2:
                min = 4;
                max = 9;

                break;
            case 3:
                min = 9;
                max = 14;
                break;
        }
        if (!FootStepSource.isPlaying)
            FootStepSource.PlayOneShot(FootStepSound[Random.Range(min - 1, max - 1)].SoundClip);
    }
    public void PlaySFX(string name)
    {
        foreach (Sound SFX in SfxSound)
        {
            if (SFX.SoundName.Contains(name))
            {
                SfxSource.PlayOneShot(SFX.SoundClip);
            }
        }
        return;
    }

    public void SetMusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        SfxSource.volume = volume;
        if (volume == 0)
            FootStepSource.volume = 0;
        else
            FootStepSource.volume = volume/10;


    }




}
