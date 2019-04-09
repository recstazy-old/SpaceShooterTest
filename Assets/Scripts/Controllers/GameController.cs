using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameController : MonoBehaviour
{
    PlayerController PlayerController { get; set; }
    PlayerModel PlayerModel { get; set; }

    public GameObject hearts;
    List<GameObject> lives = new List<GameObject>();

    void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerModel = PlayerController.PlayerModel;

        CacheLives();

        PlayerModel.HP.AsObservable()
            .Subscribe(SetLives);
    }

    void CacheLives()
    {
        for (int i = 0; i < hearts.transform.childCount; i++)
        {
            lives.Add(hearts.transform.GetChild(i).gameObject);
        }
    }

    void SetLives(int n)
    {
        if (n - 1 >= 0)
        {
            lives[n - 1].SetActive(false);

            if(n - 1 == 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}
