using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UnitButtonPresenter : ButtonPresenterBase<UnitButtonModel, UnitButtonView, UnitButtonData>
{
    public void Init(UnitButtonData data, UnitButtonView view, Vector2Int spawnStartPoint)
    {
        base.Init(data, view);
        view.isInitilazed = true;
        model.SetSpawnStartGrid(spawnStartPoint);
    }
    protected override void HandleClick()
    {
        GridCell availableCell = GridManager.Instance.FindAvailableSpawnPoint(
        model.SpawnStartGrid,
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

    }
}
