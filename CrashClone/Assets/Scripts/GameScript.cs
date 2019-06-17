
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : Singleton<GameScript> {

	public enum GameState { MainMenu,CharacterSelection, Options, Game, About}
    public delegate void OnStateChangeHandler();

    public event OnStateChangeHandler OnStateChange;

    public GameState gameState { get; private set; }

    public GameObject characterViews;
 

    public void SetGameState(GameState state)
    {
        if (state != null)
        {
            this.gameState = state;
            if (OnStateChange != null)
                OnStateChange();
        }
            
        else
            throw new UnityException();

    }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        OnStateChange += MainMenuStateHandler;
        SetGameState(GameState.MainMenu);

    }

    internal void LoadLevel(int level)
    {
        OnStateChange += LoadLevelHandler;
        SetGameState(GameState.Game);
        //avvio la scena del livello
        SceneManager.LoadScene(level);
    }

    private void LoadLevelHandler()
    {
        Debug.Log("State: Level");
        AudioManager.Instance.PlayLevelTheme();
        OnStateChange -= LoadLevelHandler;
    }

    private void MainMenuStateHandler()
    {
        Debug.Log("State: MainMenu");
        AudioManager.Instance.PlayMainTheme();
        OnStateChange -= MainMenuStateHandler;
    }

    #region MAIN MENU BUTTONS

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();

#endif
    }


    public void StartNewGame()
    {
        OnStateChange += CharacterSelectionHandler;
        SetGameState(GameState.CharacterSelection);
        
    }

    private void CharacterSelectionHandler()
    {
        Debug.Log("State: Character Selection");
        OnStateChange -= CharacterSelectionHandler;
        AudioManager.Instance.StopMainTheme();
        SetActiveCharacterViews();
        FadingMainCanvas();

    }

    private void SetActiveCharacterViews()
    {
        if(characterViews != null)
            characterViews.SetActive(true);
    }

    private void FadingMainCanvas()
    {
        var canvas = GameObject.Find("MainCanvas");
        if (canvas != null)
            canvas.SetActive(false);

        var camera = GameObject.Find("Camera");
        if (camera != null)
        {
            camera.GetComponent<CameraController>().TranslateToFirstCharacter();

        }
        else
            throw new Exception();
           
    }

    #endregion


}
