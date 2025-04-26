using UnityEngine;

public class BuildingPresenter : ConstructionPresenterBase<BuildingModel, BuildingView, BuildingConstructionData>
{

    public override void Init(BuildingConstructionData data, BuildingView view)
    {
        base.Init(data, view);

        CalculateSpawnStartPoint(); 
    }
    protected override void HandleClicked()
    {
        base.HandleClicked();
        InfoPanelManager panel = GameManager.Instance.InfoPanelManager;
        
        panel.ShowInfo(model.Data);

        if (model.CanProduceUnits)
        {
            panel.SpawnAllUnitButtons(model.SpawnStartGrid, this);
        }
        else
        {
            panel.ClearUnitButtons();
        }
    }

    protected override void HandleDestroyed()
    {
        base.HandleDestroyed();
        Debug.Log($"[BuildingPresenter] {model.BuildingType} has been destroyed.");
    }

    private void CalculateSpawnStartPoint()
    {
        Vector2 buildingGridPos = view.transform.position;
        Vector2 offset = new Vector2(
            (model.Data.dimension.x % 2 != 0) ? 0.5f : 0f,
            (model.Data.dimension.y % 2 != 0) ? 0.5f : 0f);
        buildingGridPos -= offset;
        Vector2Int buildingGrid = GridManager.Instance.WorldToGrid(buildingGridPos);
        Vector2Int startGrid = buildingGrid - (model.Data.dimension / 2);
        model.SpawnStartGrid = startGrid - Vector2Int.one;

    }


}
