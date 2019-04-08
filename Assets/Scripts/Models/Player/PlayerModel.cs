using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerModel
{
    const int startHP = 3;
    Vector2 startPosition = new Vector2Int(4, 1);

    public bool Shooting { get; set; } = true;
    
    public ReactiveProperty<int> HP { get; set; }
    public ReactiveProperty<Vector2> Position { get; set; }

    public PlayerModel()
    {
        HP = new ReactiveProperty<int>(startHP);
        Position = new ReactiveProperty<Vector2>(startPosition);
    }
}