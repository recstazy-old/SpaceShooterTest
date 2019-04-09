using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;

public class PlayerView : View
{
    PlayerModel PlayerModel { get; set; }
    SpriteRenderer Sprite { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerModel = GetComponent<PlayerController>().PlayerModel;

        PlayerModel.Position.AsObservable()
             .Subscribe(RefreshPosition);

        PlayerModel.HP.AsObservable()
            .Subscribe(StartBlinking);
    }

    public void RefreshPosition(Vector2 pos)
    {
        Rigidbody.MovePosition(pos);
    }

    void StartBlinking(int _)
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        Color spriteColor = Sprite.color;

        for(int i = 0; i < 4; i++)
        {
            Sprite.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            Sprite.color = spriteColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
