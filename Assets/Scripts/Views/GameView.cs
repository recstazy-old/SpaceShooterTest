using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameView : MonoBehaviour
{
    GameController GameController { get; set; }
    PlayerModel PlayerModel { get; set; }

    public GameObject hearts;
    List<GameObject> lives = new List<GameObject>();
    
    private void OnEnable()
    {
        if (hearts == null)
        {
            hearts = GameObject.Find("Hearts");
        }
    }

    void Start()
    {
        GameController = GetComponent<GameController>();
        PlayerModel = GameController.PlayerModel;

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
        for (int i = 0; i < n; i++)
        {
            if(!lives[i].activeSelf)
            {
                lives[i].SetActive(true);
            }
        }
        for (int i = n; i < lives.Count; i++)
        {
            if (lives[i].activeSelf)
            {
                lives[i].SetActive(false);
            }
        }
    }
}
