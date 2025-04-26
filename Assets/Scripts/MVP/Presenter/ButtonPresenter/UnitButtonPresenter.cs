using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UnitButtonPresenter : ButtonPresenterBase<UnitButtonModel, UnitButtonView, UnitButtonData>
{
    public override void Init(UnitButtonData data, UnitButtonView view)
    {
        base.Init(data, view);
        view.isInitilazed = true;
        
    }
    protected override void HandleClick()
    {
        GridCell availableCell = GridManager.Instance.FindAvailableSpawnPoint(
        GameManager.Instance.CurrentSpawnStartPoint,
        Vector2Int.one*4);
        if (availableCell == null)
        {
            Debug.LogWarning("[UnitButtonPresenter] No available cell to spawn unit!");
            return;
        }
        // Convert grid to world position
        Vector3 spawnWorldPos = GridManager.Instance.GridToWorld(availableCell.GridPosition) 
            + new Vector3(GridManager.Instance.CellSize / 2f, GridManager.Instance.CellSize / 2f);
        // Spawn unit
        GameObject unit = GenericFactory.Instance.CreateProduct(model.Data.productPrefab, spawnWorldPos);
        GridManager.Instance.SetAreaOccupied(availableCell.GridPosition, model.Data.dimension, true);
        UnitPresenter unitPresenter = new UnitPresenter();
        UnitConstructionData constructionData = GameManager.Instance.UnitConstructionDataMap[model.Data.unitType];
        UnitView unitView = unit.GetComponent<UnitView>();
        unitPresenter.Init(constructionData, unitView);
    }
}
