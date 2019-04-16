using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class MainMenuController : MonoBehaviour
{
    public delegate void UIHandler();
    public static event UIHandler OnExit;
    public static event UIHandler OnRestart;

    public GameObject mainMenu, mainMenuCanvas, lvlSelect, gameOverMassage, levelCompleteMassage, hearts, score;

    public UIData UIData { get; private set; }

    Serializer Serializer = new Serializer();

    private void OnEnable()
    {
        GameController.OnMainMenu += ShowMenuCanvas;
        GameController.OnGameOver += GameOver;
        GameController.OnLevelComplete += LevelComplete;

        UIData = new UIData();
        UIData = Serializer.DeserializeObject(UIData) as UIData;
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

    void LevelComplete()
    {
        UIData.LockedLvl.Value = SceneManager.GetActiveScene().buildIndex + 2;
        Serializer.SerializeObject(UIData);

        levelCompleteMassage.SetActive(true);
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
        Serializer.SerializeObject(UIData);

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
        score.SetActive(true);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            levelCompleteMassage.SetActive(false);
        }
    }
}
