using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerController : MonoBehaviour
{
    public delegate void AttackHandler(Vector2 pos); // Делегат передает положение игрока для корректного спауна пули
    public static event AttackHandler OnAttack;

    public PlayerModel PlayerModel { get; private set; } = new PlayerModel();

    private void Start()
    {
        InputController.TouchPos.AsObservable()
            .Subscribe(WritePosToModel);

        StartCoroutine(Attack());
    }

    void WritePosToModel(Vector2 pos)
    {
        if (pos != null)
        {
            PlayerModel.Position.Value = pos;
        }
    }

    IEnumerator Attack()
    {
        while (PlayerModel.Shooting)
        {
            OnAttack?.Invoke(PlayerModel.Position.Value);
            yield return new WaitForSeconds(1f);
        }
    }
}
