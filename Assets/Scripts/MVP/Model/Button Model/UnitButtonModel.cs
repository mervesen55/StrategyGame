using System;
using UnityEngine;

public class UnitButtonModel : ButtonModelBase<UnitButtonData>
{
    public override void Init(UnitButtonData data)
    {
        Data = data;
     
    }
    public string DisplayName => Data.displayName;

    public override Vector2Int Dimension => Data.dimension;

    public override Sprite Icon => Data.icon;

    public UnitType unitType => Data.unitType;
}
