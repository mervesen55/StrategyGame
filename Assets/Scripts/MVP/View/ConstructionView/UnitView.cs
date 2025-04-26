using UnityEngine;

public class UnitView : ConstructionViewBase
{
    public event System.Action OnViewUpdated;
    public override void SetHealth(int current, int max)
    {
    }
    private void OnMouseDown()
    {
        RaiseClickEvent();
    }

    private void Update()
    {
        OnViewUpdated?.Invoke();
    }
}