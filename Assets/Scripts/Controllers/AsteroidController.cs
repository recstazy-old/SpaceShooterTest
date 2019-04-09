using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AsteroidController : MonoBehaviour
{
    public AsteroidModel Model { get; private set; } = new AsteroidModel();
    public AsteroidView View { get; set; }

    private void Start()
    {
        View = GetComponent<AsteroidView>();
        SetProperties();

        View.RigidbodyRctv.AsObservable().Subscribe(WriteToModel);
    }

    void WriteToModel(Rigidbody2D rBody)
    {
        Model.Position = rBody.position;
        Model.RotationAngle = transform.rotation.eulerAngles.z;

        //Debug.Log(Model.Position + " rot = " + Model.RotationAngle);
    }

    void SetProperties()
    {
        Model.CurrentHP = 3;
        float speedMultipler = Random.Range(0.9f, 1.1f);
        
        if (View != null)
        {
            View.Rigidbody.velocity = Vector2.down * Model.Speed * speedMultipler;
            View.Rigidbody.angularVelocity = Model.Speed * speedMultipler * 3f;
            if (RandomBool())
            {
                View.Rigidbody.angularVelocity = -View.Rigidbody.angularVelocity; // Изменяет направление вращения метеорита
            }
        }
    }

    bool RandomBool()
    {
        bool result = false;
        int n = Random.Range(0, 100);
        if (n <= 30)
        {
            result = !result;
        }
        return result;
    }

    private void OnEnable()
    {
        SetProperties();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if(Model.CurrentHP > 1)
            {
                Model.CurrentHP--;
            }
            else if(Model.CurrentHP <= 1)
            {
                gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Player" || collision.gameObject.name == "BottomWall")
        {
            gameObject.SetActive(false);
        }
    }
}
