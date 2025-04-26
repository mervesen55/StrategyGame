using System;
using UnityEngine;

public class UnitButtonModel : ButtonModelBase<UnitButtonData>
{
    public Vector2Int SpawnStartGrid { get; private set; }
    public override void Init(UnitButtonData data)
    {
        Data = data;
     
    }
    public void SetSpawnStartGrid(Vector2Int gridPos)
    {
        SpawnStartGrid = gridPos;
    }
    public string DisplayName => Data.displayName;

    public override Vector2Int Dimension => Data.dimension;

    public override Sprite Icon => Data.icon;

    public UnitType unitType => Data.unitType;
}
