using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public static SettingUI Instance;
    public Slider Music, SFX;
    public Button Exit, Resume;
    public bool isactive;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        setNewVolume();
        MusicVolume();
        SFXVolume();

    }

    private void setNewVolume()
    {
        Music.value = AudioManager.Instance.MusicSource.volume;
        SFX.value = AudioManager.Instance.SfxSource.volume;
    }

    public void MusicVolume()
    {
        AudioManager.Instance.SetMusicVolume(Music.value);

    }
    public void SFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(SFX.value);
    }

    public void resume()
    {
        isactive = !isactive;
        this.gameObject.SetActive(false);
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }
    public void exit()
    {
        Time.timeScale = 1;
        Operator.Instance.Exit();

    }


}
