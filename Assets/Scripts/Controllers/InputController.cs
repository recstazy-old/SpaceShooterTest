using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class InputController : MonoBehaviour
{
    public delegate void EscapeHandler();
    public static event EscapeHandler OnEscape;

    // Attached to EventSystem gameObj

    public static ReactiveProperty<Vector2> TouchPos { get; private set; } = new ReactiveProperty<Vector2>();
    
    private void Start()
    {
        this.UpdateAsObservable()
            .Where(x => Input.touchCount > 0)
            .Subscribe(OnTouch);
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(Escape);
    }

    void Escape(Unit _)
    {
        OnEscape?.Invoke();
    }

    void OnTouch(Unit x)
    {
        TouchPos.Value = ScalePosition(Input.GetTouch(0).position);
    }

    Vector2 ScalePosition(Vector2 rawPos)
    {
        return new Vector2(rawPos.x / Screen.width * 8f, rawPos.y / Screen.height * 14f);
    }
}
