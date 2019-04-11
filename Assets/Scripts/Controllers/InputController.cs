using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class InputController : MonoBehaviour
{
    // Attached to EventSystem gameObj

    public static ReactiveProperty<Vector2> TouchPos { get; private set; } = new ReactiveProperty<Vector2>();
    
    private void Start()
    {
        this.UpdateAsObservable()
            .Where(x => Input.touchCount > 0)
            .Subscribe(OnTouch);
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
