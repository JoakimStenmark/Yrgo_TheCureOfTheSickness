// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    
    public enum GameState {

        Menu,
        LoadLevel,
        GameLoop,
        PauseGame,
        GameOver
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

            case GameState.GameLoop:

                break;

            case GameState.PauseGame:

                break;

            case GameState.GameOver:

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


        }

        if( newGameState == GameState.LoadLevel ) {

            SceneManager.LoadScene("RobinTestScene");

        }

        if( newGameState == GameState.GameLoop ) {


        }

        if( newGameState == GameState.PauseGame ) {


        }

        if( newGameState == GameState.GameOver ) {


        }

        currentGameState = newGameState;
    }
}
