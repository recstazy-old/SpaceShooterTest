using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : View
{
    BulletModel Model { get; set; }

    private void Awake()
    {
        Model = GetComponent<BulletController>().Model;
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
