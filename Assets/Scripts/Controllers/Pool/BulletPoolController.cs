using UnityEngine;
using UniRx;

public class BulletPoolController : Pool
{
    private void Start()
    {
        AddChildrenToList();
        PlayerController.OnAttack += SpawnObject;

        GameController.OnRestart += KillAllObjects;
    }
}
