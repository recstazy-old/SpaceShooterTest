using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;

public class PlayerView : View
{
    PlayerModel PlayerModel { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerModel = GetComponent<PlayerController>().PlayerModel;

        PlayerModel.Position.AsObservable()
             .Subscribe(RefreshPosition);
    }

    public void RefreshPosition(Vector2 pos)
    {
        Rigidbody.MovePosition(pos);
    }
}
