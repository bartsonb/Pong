using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Game Game;
    private Settings Settings;

    public void StartGame() 
    {
        SceneManager.LoadScene("PongGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
