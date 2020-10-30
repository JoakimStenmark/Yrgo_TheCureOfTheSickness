// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    void Start() {

        if( GameManager.instance.currentGameState == GameManager.GameState.Menu ) {

            
        }
    }

    public void StartGame() {
        
        GameManager.instance.ChangeGameState(GameManager.GameState.LoadLevel);
    }

    public void QuitGame() {

        Application.Quit();
    }

    public void MainMenu() {
  
        GameManager.instance.ChangeGameState( GameManager.GameState.Menu );
    }
}
