using UnityEngine;

public class BuildingModel : ConstructionModelBase<BuildingConstructionData>
{  
    public override void Init(BuildingConstructionData data)
    {
        if (data == null)
        {
            Debug.LogError("BuildingModel.Init: data is NULL!");
            return;
        }

        base.Init(data);
        CurrentHealth = data.MaxHealth;
    }

    public bool CanProduceUnits => Data.canProduce;
    public BuildingType BuildingType => Data.buildingType;

}
