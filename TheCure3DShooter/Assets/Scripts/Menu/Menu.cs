// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public void StartGame() {
        
        GameManager.instance.ChangeGameState(GameManager.GameState.LoadLevel);
    }

    public void QuitGame() {

        Debug.Log("quit");

        #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }

    public void MainMenu() {
  
        GameManager.instance.ChangeGameState( GameManager.GameState.Menu );
    }
}
