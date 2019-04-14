using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameController : MonoBehaviour
{
    public delegate void GameControllerHandler();
    public static event GameControllerHandler OnGameOver;
    public static event GameControllerHandler OnRestart;
    public static event GameControllerHandler OnMainMenu;

    Serializer Serializer { get; set; } = new Serializer();
    PlayerController PlayerController { get; set; }
    public PlayerModel PlayerModel { get; private set; }

    public static bool isGameOver = false;

    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        DeserializeAll();

        PlayerModel = PlayerController.PlayerModel;
    }

    void Start()
    {
        PlayerModel.HP.AsObservable()
            .Where(_ => PlayerModel.HP.Value == 0)
            .Subscribe(GameOver);
    }

    private void OnEnable()
    {
        InputController.OnEscape += MainMenu;
        MainMenuController.OnExit += ExitGame;
        MainMenuController.OnRestart += Restart;
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
        Debug.Log("Serialized");
    }

    void DeserializeAll()
    {
        PlayerController.PlayerModel = Serializer.DeserializeObject(PlayerController.PlayerModel) as PlayerModel;
    }

    private void OnDestroy()
    {
        InputController.OnEscape -= MainMenu;
        MainMenuController.OnExit -= ExitGame;
        MainMenuController.OnRestart -= Restart;
    }
}
