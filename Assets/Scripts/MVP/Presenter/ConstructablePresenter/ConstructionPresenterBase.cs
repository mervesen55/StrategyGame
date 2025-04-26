using System;
using UnityEngine;

public abstract class ConstructionPresenterBase<TModel, TView, TData>
    where TModel : ConstructionModelBase<TData>, new()
    where TView : ConstructionViewBase
    where TData : ConstructionProductData
{
    protected TModel model;
    protected TView view;

    public virtual void Init(TData data, TView view)
    {
        this.view = view;
        model = new TModel();
        model.Init(data);
        Debug.Log("init of construction presenter base worked");
        view.SetHealth(model.CurrentHealth, model.Data.MaxHealth);
        view.OnClicked += HandleClicked;
    }

    public virtual void TakeDamage(int amount)
    {
        model.IncreaseHealth(amount);
        view.SetHealth(model.CurrentHealth, model.Data.MaxHealth);

        if (model.IsDestroyed)
            HandleDestroyed();
    }

  
    protected virtual void HandleClicked()
    {
        Debug.Log($"Clicked on {typeof(TModel).Name}");
    }

    protected virtual void HandleDestroyed()
    {
        Debug.Log($"{typeof(TModel).Name} destroyed!");
    }
}
