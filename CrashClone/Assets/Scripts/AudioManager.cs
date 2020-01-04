
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField] private AudioSource mainThemeSource;
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip characterTheme;
    [SerializeField] private AudioClip levelTheme;
    [SerializeField] private AudioClip optionsTheme;
    [SerializeField] private AudioClip world;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip; //da aggiungerne altri?

    private void Awake()
    {
        OnReload();
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        //subscribe to the eventManager event when changing view
     
        

    }

    public void ConnectAudioManager()
    {
        GameScript.Instance.OnStateChange += PlayViewTheme;
    }

    private void PlayViewTheme()
    {
        //stop della canzone attuale
        StopMainTheme();
        

        switch (GameScript.Instance.gameState)
        {
            case GameScript.GameState.MainMenu: PlayMainTheme();
                break;
            case GameScript.GameState.CharacterSelection: PlayCharacterTheme();
                break;
            case GameScript.GameState.Options: PlayOptionTheme();
                break;
            case GameScript.GameState.Game: PlayLevelTheme();
                break;
                //da aggiungere altri
            default:
                break;
        }
    }

    private void PlayOptionTheme()
    {
        mainThemeSource.clip = optionsTheme;  
        mainThemeSource.Play();
    }

    private void PlayCharacterTheme()
    {
        mainThemeSource.clip = optionsTheme;  
        mainThemeSource.Play();
    }

    private void PlayMainTheme()
    {
        mainThemeSource.clip = mainTheme;
        mainThemeSource.Play();
    }

  
    private void PlayLevelTheme()
    {
        mainThemeSource.clip = levelTheme;  
        mainThemeSource.Play();  
    }

    private void StopMainTheme()
    {
        mainThemeSource.Stop();
    }
}
