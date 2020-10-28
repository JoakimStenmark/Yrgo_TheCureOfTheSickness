using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void ChangeGameState( GameState newGameState ) {

        if( currentGameState == newGameState ) {

            return;
        }

        if( newGameState == GameState.Menu ) {


        }

        if( newGameState == GameState.LoadLevel ) {

            
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
