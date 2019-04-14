using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public delegate void UIHandler();
    public static event UIHandler OnExit;
    public static event UIHandler OnRestart;

    public GameObject mainMenu, mainMenuCanvas, lvlSelect, gameOverMassage, hearts;

    private void OnEnable()
    {
        GameController.OnMainMenu += ShowMenuCanvas;
        GameController.OnGameOver += GameOver;
    }

    public void ShowLvls()
    {
        mainMenu.SetActive(false);
        lvlSelect.SetActive(true);
    }

    public void ShowMainMenu()
    {
        lvlSelect.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowMenuCanvas()
    {
        if (!mainMenuCanvas.activeSelf)
        {
            Time.timeScale = 0;
            lvlSelect.SetActive(false);
            mainMenu.SetActive(true);
            mainMenuCanvas.SetActive(true);
        }
        else
        {
            mainMenuCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void GameOver()
    {
        gameOverMassage.SetActive(true);
    }

    public void Restart()
    {
        OnRestart?.Invoke();
        gameOverMassage.SetActive(false);
    }

    public void Exit()
    {
        if (OnExit == null)
        {
            Application.Quit();
        }
        else
        {
            OnExit?.Invoke();
        }
    }

    public void LoadScene(int i)
    {
        hearts.SetActive(true);
        SceneManager.LoadScene(i, LoadSceneMode.Single);
        mainMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
