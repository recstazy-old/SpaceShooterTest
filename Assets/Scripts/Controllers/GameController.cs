using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public delegate void GameControllerHandler();
    public static event GameControllerHandler OnGameOver;
    public static event GameControllerHandler OnLevelComplete;
    public static event GameControllerHandler OnRestart;
    public static event GameControllerHandler OnMainMenu;

    Serializer Serializer { get; set; } = new Serializer();
    PlayerController PlayerController { get; set; }
    public PlayerModel PlayerModel { get; private set; }
    public GameData GameData { get; private set; }

    public static bool isGameOver = false;

    private void Awake()
    {
        GameData = new GameData();
        
        PlayerController = FindObjectOfType<PlayerController>();
        DeserializeAll();

        PlayerModel = PlayerController.PlayerModel;
    }

    void Start()
    {
        PlayerModel.HP.AsObservable()
            .Where(_ => PlayerModel.HP.Value == 0)
            .Subscribe(GameOver);

        GameData.Score.AsObservable()
            .Where(x => GameData.Score.Value <= 0)
            .Subscribe(LevelComplete);
    }

    private void OnEnable()
    {
        InputController.OnEscape += MainMenu;
        MainMenuController.OnExit += ExitGame;
        MainMenuController.OnRestart += Restart;
        AsteroidController.OnAsteroidKilled += AsteroidKilled;
    }

    void AsteroidKilled()
    {
        GameData.Score.Value--;
    }

    void LevelComplete(int score)
    {
        Time.timeScale = 0;
        GameData.Score.Value = 10;
        SerializeAll();
        OnLevelComplete?.Invoke();
    }

    void GameOver(int _)
    {
        isGameOver = true;
        Time.timeScale = 0;
        OnGameOver?.Invoke();
    }

    public void Restart()
    {
        isGameOver = false;
        OnRestart?.Invoke();
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        if (PlayerModel.HP.Value > 0)
        {
            SerializeAll();
        }
        else
        {
            PlayerModel.SetDefaults();
            SerializeAll();
        }

        Application.Quit();
    }

    void MainMenu()
    {
        if (!isGameOver)
        {
            Time.timeScale = 0;
            OnMainMenu?.Invoke();
        }
    }

    void SerializeAll()
    {
        Serializer.SerializeObject(PlayerModel);

        GameData.CurrentScene = SceneManager.GetActiveScene().name;
        GameData.SavedScore = GameData.Score.Value;
        Serializer.SerializeObject(GameData);
    }

    void DeserializeAll()
    {
        PlayerController.PlayerModel = Serializer.DeserializeObject(PlayerController.PlayerModel) as PlayerModel;

        GameData = Serializer.DeserializeObject(GameData) as GameData;
        GameData.Score.Value = GameData.SavedScore;

        if(GameData.CurrentScene != SceneManager.GetActiveScene().name)
        {
            GameData.CurrentScene = SceneManager.GetActiveScene().name;
            GameData.Score.Value = GameData.StartScore;
            PlayerController.PlayerModel.SetDefaults();
        }
    }

    private void OnDestroy()
    {
        InputController.OnEscape -= MainMenu;
        MainMenuController.OnExit -= ExitGame;
        MainMenuController.OnRestart -= Restart;
        AsteroidController.OnAsteroidKilled -= AsteroidKilled;
    }
}
