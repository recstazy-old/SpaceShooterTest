using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameController : MonoBehaviour
{
    public delegate void GameControllerHandler();
    public static event GameControllerHandler OnGameOver;
    public static event GameControllerHandler OnRestart;

    Serializer Serializer { get; set; } = new Serializer();
    PlayerController PlayerController { get; set; }
    public PlayerModel PlayerModel { get; private set; }

    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerController.PlayerModel = Serializer.DeserializeObject(PlayerController.PlayerModel) as PlayerModel;

        PlayerModel = PlayerController.PlayerModel;
    }

    void Start()
    {
        InputController.OnEscape += Escape;
        
        PlayerModel.HP.AsObservable()
            .Where(_ => PlayerModel.HP.Value == 0)
            .Subscribe(GameOver);
    }

    void GameOver(int _)
    {
        Time.timeScale = 0;
        OnGameOver?.Invoke();
    }

    public void Restart()
    {
        OnRestart?.Invoke();
        Time.timeScale = 1;
    }

    void Escape()
    {
        Debug.Log("Escape");

        if (PlayerModel.HP.Value > 0)
        {
            SerializeAll();
        }

        Application.Quit();
    }

    void SerializeAll()
    {
        Serializer.SerializeObject(PlayerModel);
        Application.Quit();
        Debug.Log("Serialized");
    }
}
