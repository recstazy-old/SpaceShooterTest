using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ButtonController : MonoBehaviour
{
    Button Button { get; set; }
    int buttonNumber;

    MainMenuController MenuController { get; set; }
    UIData UIData { get; set; }

    private void Awake()
    {
        MenuController = FindObjectOfType<MainMenuController>();
        UIData = MenuController.UIData;

        Button = GetComponent<Button>();
        Button.interactable = false;
    }

    private void Start()
    {
        buttonNumber = GetComponent<Number>().n;

        UIData.LockedLvl.AsObservable()
            .Subscribe(SetActive);
    }

    void SetActive(int n)
    {
        if (buttonNumber < n)
        {
            Button.interactable = true;
        }
    }
}
