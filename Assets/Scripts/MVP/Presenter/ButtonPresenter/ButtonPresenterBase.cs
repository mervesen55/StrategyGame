using System;
using UnityEngine;

public abstract class ButtonPresenterBase<TModel, TView, TData>
    where TModel : ButtonModelBase<TData>, new()
    where TView : ButtonViewBase
    where TData : ScriptableObject
{
    protected TModel model;
    protected TView view;

    public virtual void Init(TData data, TView view)//Init(Enum type, TView view)
    {
        this.view = view;
        this.model = new TModel();
        model.Init(data);
        view.SetIcon(model.Icon);
        view.SetName((data as UIProductData)?.displayName ?? "Unnamed");
        view.OnClicked -= HandleClick;
        view.OnClicked += HandleClick;
    }

    protected abstract void HandleClick();
}
