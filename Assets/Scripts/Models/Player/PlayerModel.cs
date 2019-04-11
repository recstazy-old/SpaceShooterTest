using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerModel
{
    const int startHP = 3;
    Vector2 startPosition = new Vector2Int(4, 1);

    public bool Shooting { get; set; } = true;
    
    public ReactiveProperty<int> HP { get; set; } = new ReactiveProperty<int>();
    public ReactiveProperty<Vector2> Position { get; set; } = new ReactiveProperty<Vector2>();

    public PlayerModel()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        HP.Value = startHP;
        Position.Value = startPosition;
    }
}