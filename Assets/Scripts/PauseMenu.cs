using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Canvas")]
    public GameObject pauseMenuUI;
    public bool gameIsPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)

                ResumeGame();
            else
                GamePaused();
        }
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Game is Resumed");
    }

    public void GamePaused()
    {
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0;
        Debug.Log("Game isPaused");
    }

    public void QuiteButton()
    {
        Application.Quit(); 
    }
}
