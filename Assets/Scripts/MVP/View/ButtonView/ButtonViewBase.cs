using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonViewBase : MonoBehaviour
{
    public event Action OnClicked;

    public abstract void SetIcon(Sprite sprite);
    public abstract void SetName(string text);

    public void Click()
    {
        OnClicked?.Invoke();
    }
}
