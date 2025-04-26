using System;
using UnityEngine;

public abstract class ConstructionViewBase : MonoBehaviour
{
    public event Action OnClicked;

    public abstract void SetHealth(int current, int max);

    protected void RaiseClickEvent()
    {
        OnClicked?.Invoke();
    }
}
