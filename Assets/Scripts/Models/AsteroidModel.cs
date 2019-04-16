using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.SceneManagement;

public class AsteroidModel
{
    public int StartHP
    {
        get
        {
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                return 3;
            }
            else if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                return 5;
            }

            return 2;
        }
    }



    public Color StartColor { get; private set; } = new Color(1f, 0.2783019f, 0.3963421f, 1f);
    public ReactiveProperty<int> CurrentHP { get; set; } = new ReactiveProperty<int>();

    public Vector2 Position { get; set; }
    public float RotationAngle { get; set; }
    public int Speed { get; private set; } = 4;

    public AsteroidModel()
    {
        CurrentHP.Value = 2;
    }
}
