using System;
using UnityEngine;

public abstract class ButtonModelBase<TData> where TData : ScriptableObject
{
    public TData Data { get; protected set; }

    public abstract void Init(TData data); //  BuildingType, UnitType etc.

    public abstract Vector2Int Dimension { get; }
    public abstract Sprite Icon { get; }

    

}
