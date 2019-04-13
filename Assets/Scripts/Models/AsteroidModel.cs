using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class AsteroidModel
{
    int StartHP { get; set; } = 3;
    public Color StartColor { get; private set; } = new Color(1f, 0.2783019f, 0.3963421f, 1f);
    //public int CurrentHP { get; set; }
    public ReactiveProperty<int> CurrentHP { get; set; } = new ReactiveProperty<int>();

    public Vector2 Position { get; set; }
    public float RotationAngle { get; set; }
    public int Speed { get; private set; } = 4;

    public AsteroidModel()
    {
        CurrentHP.Value = StartHP;
    }
}
