
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField] private AudioSource mainThemeSource;
    [SerializeField] private AudioClip mainTheme;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip; //da aggiungerne altri?

    private void Awake()
    {
        OnReload();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMainTheme()
    {
        mainThemeSource.clip = mainTheme;
        mainThemeSource.Play();
    }

    public void StopMainTheme()
    {
        mainThemeSource.Stop();
    }

    internal void PlayLevelTheme()
    {
        mainThemeSource.clip = mainTheme;  //TODO da cambiare musica
        mainThemeSource.Play();  
    }
}
