using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Pool : MonoBehaviour
{
    protected ReactiveCollection<GameObject> List { get; set; } = new ReactiveCollection<GameObject>();

    protected void AddToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            List.Add(transform.GetChild(i).gameObject);
        }
    }

    protected void SpawnObject(Vector2 pos)  // pos - координаты появления объекта
    {
        foreach (GameObject obj in List)
        {
            if (!obj.activeSelf)
            {
                obj.transform.position = pos;
                obj.SetActive(true);
                break;
            }
        }
    }
}
