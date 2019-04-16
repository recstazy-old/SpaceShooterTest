using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletModel Model { get; private set; } = new BulletModel();
    BulletView View { get; set; }

    private void Start()
    {
        View = GetComponent<BulletView>();
        View.Rigidbody.velocity = Vector2.up * Model.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (View != null)
        {
            View.Rigidbody.velocity = Vector2.up * Model.speed;
            View.Trail.Clear();
        }
    }

    //private void OnDisable()
    //{
        
    //}
}
