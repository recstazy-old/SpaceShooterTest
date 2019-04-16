using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

[Serializable]
public class UIData
{
    public ReactiveProperty<int> LockedLvl { get; set; } = new ReactiveProperty<int>();
    
    public UIData()
    {
        LockedLvl.Value = 2;
    }
}
