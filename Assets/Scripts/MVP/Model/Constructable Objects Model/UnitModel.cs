using System.Collections.Generic;
using UnityEngine;

public class UnitModel : ConstructionModelBase<UnitConstructionData>
{
   
    public void SetSpawnStartGrid(Vector2Int gridPos)
    {
        SpawnStartGrid = gridPos;
    }

}


