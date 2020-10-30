// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    AudioSource audioSource;

    void Start() {

        audioSource = GetComponent<AudioSource>();

        if( GameManager.instance.currentGameState == GameManager.GameState.Menu ) {

            
        }
    }

    public void StartGame() {
        
        GameManager.instance.ChangeGameState(GameManager.GameState.LoadLevel);
        Debug.Log("loading scene");
    }

    public void QuitGame() {

        Application.Quit();
    }

    public void MainMenu() {
  
        GameManager.instance.ChangeGameState( GameManager.GameState.Menu );
    }
}
