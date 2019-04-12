using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerView : View
{
    PlayerController PlayerController { get; set; }
    PlayerModel PlayerModel { get; set; }
    SpriteRenderer Sprite { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerModel = PlayerController.PlayerModel;

        this.UpdateAsObservable()
            .Where(_ => Rigidbody.position != PlayerModel.Position.Value)
            .Subscribe(LerpToPosition);

        PlayerModel.HP.AsObservable()
            .Where(_ => PlayerModel.HP.Value < 3)
            .Subscribe(StartBlinking);
    }

    void LerpToPosition(Unit _)
    {
        //Moves view to position from model
        Rigidbody.MovePosition(Vector2.Lerp(Rigidbody.position, PlayerModel.Position.Value, 0.3f));
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
            Sprite.color = Color.black;
            yield return new WaitForSeconds(0.05f);
            Sprite.color = spriteColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
