using UnityEngine;
using UniRx;

public class BulletPoolController : Pool
{
    //protected ReactiveCollection<GameObject> Bullets { get; set; } = new ReactiveCollection<GameObject>();

    //PlayerController PlayerController { get; set; }
    //PlayerModel PlayerModel { get; set; }

    private void Start()
    {
        //PlayerController = FindObjectOfType<PlayerController>();
        //PlayerModel = PlayerController.PlayerModel;

        AddToList();

        PlayerController.OnAttack += SpawnObject;
    }

    
}
