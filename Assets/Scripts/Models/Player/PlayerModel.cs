using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerModel
{
    const int startHP = 3;
    Vector2 startPosition = new Vector2Int(4, 1);

    public bool Shooting { get; set; } = true;

    public float AttackSpeed
    {
        get
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                return 0.4f;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                return 0.3f;
            }

            return 0.5f;
        }
    }
    
    public ReactiveProperty<int> HP { get; set; } = new ReactiveProperty<int>();
    public ReactiveProperty<Vector2> Position { get; set; } = new ReactiveProperty<Vector2>();

    public PlayerModel()
    {
        //Debug.Log("Constructor");
        SetDefaults();
    }

    public void SetDefaults()
    {
        HP.Value = startHP;
        Position.Value = startPosition;
    }
}