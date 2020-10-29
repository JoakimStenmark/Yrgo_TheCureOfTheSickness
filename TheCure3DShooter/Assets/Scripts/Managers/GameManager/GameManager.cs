// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Canvas menuCanvas;

    public enum GameState {

        Menu,
        LoadLevel,
        LevelLoaded,
        GameLoop,
        PauseGame,
        GameOver,
        Victory
    }
    [Header("GameState")]
    public GameState currentGameState;
    [Space(10)]
    [Header("Options")]
    public bool debugActive;

    void Awake() {

        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this.gameObject );
        }
    }

    void Update() {

        switch( currentGameState ) {

            case GameState.Menu:

                break;

            case GameState.LoadLevel:

                break;

            case GameState.LevelLoaded:

                break;

            case GameState.GameLoop:

                break;

            case GameState.PauseGame:

                break;

            case GameState.GameOver:

                break;

            case GameState.Victory:

                break;

            default:
                break;
        }
    }

    public void ChangeGameState( GameState newGameState ) {

        if( currentGameState == newGameState ) {

            return;
        }

        if( newGameState == GameState.Menu ) {

            SceneManager.LoadScene( "Menu" );
            Cursor.visible = true;
        }

        if( newGameState == GameState.LoadLevel ) {

            Cursor.visible = false;
            SceneManager.LoadScene( "GameLevel" );
            menuCanvas.gameObject.SetActive( false );
        }

        if ( newGameState == GameState.LevelLoaded ) {


        }

        if( newGameState == GameState.GameLoop ) {


        }

        if( newGameState == GameState.PauseGame ) {


        }

        if( newGameState == GameState.GameOver ) {

            Cursor.visible = true;
            menuCanvas.gameObject.SetActive( true );
        }

        if (newGameState == GameState.Victory) {

            SceneManager.LoadScene("WinnerLevel");
        }

        currentGameState = newGameState;
    }
}
