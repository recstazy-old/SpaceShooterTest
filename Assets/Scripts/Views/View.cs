using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class View : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; protected set; }
    public SpriteRenderer Sprite { get; protected set; }
}
