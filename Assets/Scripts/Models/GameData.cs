using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class GameData
{
    public string CurrentScene { get; set; }
    public int SavedScore { get; set; }

    public static ReactiveProperty<int> Score { get; set; } = new ReactiveProperty<int>();

    public int StartScore
    {
        get
        {
            if (CurrentScene == "lvl0" || CurrentScene == "lvl1")
            {
                return 10;
            }
            else if (CurrentScene == "lvl2")
            {
                return 15;
            }
            else if (CurrentScene == "lvl3")
            {
                return 20;
            }

            return 10;
        }
    }

    public GameData()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        Score.Value = StartScore;
        CurrentScene = "lvl0";
    }
}
