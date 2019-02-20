using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeUI : MonoBehaviour {

    public GameObject welcomeMessage;
    public GameObject pauseMenu;
    public GameObject winMessage;
    public GameObject loseMessage;
    public GameObject resumeButton;
    private PlayerUIComponent playerUIComponent;
    private bool gamePaused;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        playerUIComponent = GameObject.FindGameObjectWithTag("PlayerTank").GetComponent<PlayerUIComponent>();
        gamePaused = false;
    }
	

    public IEnumerator WelcomeMessage()
    {
        welcomeMessage.SetActive(true);
        yield return new WaitForSeconds(3f);
        welcomeMessage.SetActive(false);
    }

    public void GameWin()
    {
        winMessage.SetActive(true);
        resumeButton.SetActive(false);
        PopUpPauseMenu();
    }


    public void PopUpPauseMenu()
    {
        Time.timeScale = (Time.timeScale == 0f)? 1f : 0f;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if(pauseMenu.activeSelf == true)
        {
            playerUIComponent.EnableCursor();
        }
        else
        {
            playerUIComponent.DisableCursor();
        }

        gamePaused = !gamePaused;

    }

    public void GameLose()
    {
        loseMessage.SetActive(true);
        resumeButton.SetActive(false);
        PopUpPauseMenu();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }

}
