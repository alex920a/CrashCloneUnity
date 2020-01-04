
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : Singleton<GameScript> {

    //stati del gioco
	public enum GameState { MainMenu,CharacterSelection, Options, Game, About}
    public GameState gameState { get; private set; }
    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;

    public GameObject shadowPanel;
    private CanvasGroup shadowCanvasGroup;
    private float step = 2.0f;

    
    //utilizzato nella schermata di selezione personaggio
    public GameObject characterViews;
 

    //Metodo pubblico per impostare da fuori lo stato del gioco
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


    //il game manager deve rimanere in vita in tutte le scene
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    //metodo che setta lo stato della schermata iniziale
    private void Start()
    {
        OnStateChange += MainMenuStateHandler;
        AudioManager.Instance.ConnectAudioManager();
        SetGameState(GameState.MainMenu);
        

    }

    internal void LoadLevel(int level)
    {
        OnStateChange += LoadLevelHandler;
        SetGameState(GameState.Game);
        //avvio la scena del livello
        SceneManager.LoadScene(level);
     
    }


    #region HANDLERS

  
    //MAIN MENU
    private void MainMenuStateHandler()
    {
        Debug.Log("State: MainMenu");
        OnStateChange -= MainMenuStateHandler;
    }

    //CHARACTER SELECTION
    private void CharacterSelectionHandler()
    {
        Debug.Log("State: Character Selection");
        OnStateChange -= CharacterSelectionHandler;
       
        SetActiveCharacterViews();
        FadingMainCanvas();

    }


    //LEVEL
    private void LoadLevelHandler()
    {
        Debug.Log("State: Level");
      
        OnStateChange -= LoadLevelHandler;
    }



    #endregion

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

    

    //attivo il gameObject characterViews in scena
    private void SetActiveCharacterViews()
    {
        if(characterViews != null)
            characterViews.SetActive(true);
    }



    private void FadingMainCanvas()
    {
        shadowCanvasGroup = shadowPanel.GetComponent<CanvasGroup>();
        if (shadowCanvasGroup == null)
            throw new UnityException();


      
        var canvas = GameObject.Find("MainCanvas");
        if (canvas != null)
            canvas.SetActive(false); //disabilito il canvas della scheramata iniziale

        var camera = GameObject.Find("Camera");
        if (camera != null)
            camera.GetComponent<CameraController>().TranslateToFirstCharacter(); //traslo la camera al primo personaggio
        
        else
            throw new Exception();

       // StartCoroutine(FadingInOutCanvas(step));

    }

    private IEnumerator FadingInOutCanvas(float step)
    {
        shadowPanel.SetActive(true);

        while (shadowCanvasGroup.alpha < 1)
        {
            shadowCanvasGroup.alpha += step;
            yield return null;
        }

        yield return new WaitForSeconds(3);

        while (shadowCanvasGroup.alpha > 0)
        {
            shadowCanvasGroup.alpha -= step;
            yield return null;
        }

        shadowPanel.SetActive(false);


    }

    #endregion


}
