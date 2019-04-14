using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : View
{
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
