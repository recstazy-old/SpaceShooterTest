using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AsteroidView : View
{
    public ReactiveProperty<Rigidbody2D> RigidbodyRctv { get; private set; } = new ReactiveProperty<Rigidbody2D>();

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        Debug.Log(Rigidbody);
        RigidbodyRctv.Value = Rigidbody;
    }

    void Start()
    {
        
    }
}
