// ROBIN B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

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
