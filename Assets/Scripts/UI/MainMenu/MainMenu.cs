using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindObjectOfType<GameManager>() == null)
        {
            GameObject gameManager = new("GameManager");
            gameManager.AddComponent<GameManager>();
        }
    }

    public void PlayGame()
    {
        if (!GameManager.cutscenePlayed)
        {
            SceneManager.LoadSceneAsync("IntroCutscene");
            GameManager.cutscenePlayed = true;
        }
        else
        {
            SceneManager.LoadSceneAsync("HouseMc");
        }
    }

    public void BackHome()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is Closed");
    }
}
