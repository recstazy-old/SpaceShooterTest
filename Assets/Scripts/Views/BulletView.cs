using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : View
{
    public TrailRenderer Trail { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Trail = GetComponent<TrailRenderer>();
    }
}
