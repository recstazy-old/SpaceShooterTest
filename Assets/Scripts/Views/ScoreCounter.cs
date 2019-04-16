using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    TextMeshProUGUI Score { get; set; }

    GameObject GameController { get; set; }
    GameData GameData { get; set; }
    
    private void OnEnable()
    {
        Score = GetComponent<TextMeshProUGUI>();

        GameData.Score.AsObservable()
            .Subscribe(RefreshScore);
    }

    void RefreshScore(int score)
    {
        Score.text = "Asteroids Left: " + score;
    }
}
