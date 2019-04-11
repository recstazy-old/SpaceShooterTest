using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameController : MonoBehaviour
{
    public delegate void GameControllerHandler();
    public static event GameControllerHandler OnGameOver;
    public static event GameControllerHandler OnRestart;


    PlayerController PlayerController { get; set; }
    public PlayerModel PlayerModel { get; private set; }

    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerModel = PlayerController.PlayerModel;
    }

    void Start()
    {
        PlayerModel.HP.AsObservable()
            .Where(_ => PlayerModel.HP.Value == 0)
            .Subscribe(GameOver);
    }

    void GameOver(int _)
    {
        Debug.Log(PlayerModel.HP.Value);
        Time.timeScale = 0;
        OnGameOver?.Invoke();
    }

    public void Restart()
    {
        OnRestart?.Invoke();
        Time.timeScale = 1;
    }
}
