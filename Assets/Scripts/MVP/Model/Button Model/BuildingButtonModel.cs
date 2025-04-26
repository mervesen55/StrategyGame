using System;
using UnityEngine;

public class BuildingButtonModel : ButtonModelBase<BuildingButtonData>
{
    public override void Init(BuildingButtonData data)
    {
        Data = data;
    }

    public string DisplayName => Data.displayName;

    public override Vector2Int Dimension => Vector2Int.RoundToInt(Data.dimension);

    public override Sprite Icon => Data.icon;

    public BuildingType BuildingType => Data.buildingType;


}
