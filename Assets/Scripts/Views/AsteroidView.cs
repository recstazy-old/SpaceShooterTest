using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AsteroidView : View
{
    public ReactiveProperty<Rigidbody2D> RigidbodyRctv { get; private set; } = new ReactiveProperty<Rigidbody2D>();
    AsteroidController Controller { get; set; }
    AsteroidModel Model { get; set; }

    private void Awake()
    {
        Controller = GetComponent<AsteroidController>();
        Model = Controller.Model;

        Sprite = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        RigidbodyRctv.Value = Rigidbody;
    }

    private void Start()
    {
        Model.CurrentHP.AsObservable()
            .Where(_ => Model.CurrentHP.Value > 0)
            .Subscribe(Blink);
    }

    private void OnEnable()
    {
        Sprite.color = Model.StartColor;
    }

    private void OnDisable()
    {
        Sprite.color = Model.StartColor;
    }

    void Blink(int _)
    {
        StartCoroutine(ColorBlink.Blink(Sprite, Color.gray, 0.025f));
    }
}
