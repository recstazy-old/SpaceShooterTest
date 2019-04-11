using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AsteroidPoolController : Pool
{
    private void Start()
    {
        AddChildrenToList();
        StartCoroutine(AsteroidSpawner());

        GameController.OnRestart += KillAllObjects;
    }

    IEnumerator AsteroidSpawner()
    {
        while(true)
        {
            float x = Random.Range(0.5f, 7.5f);
            Vector2 position = new Vector2(x, 16f);
            SpawnObject(position);
            yield return new WaitForSeconds(2f);
        }
    }
}
