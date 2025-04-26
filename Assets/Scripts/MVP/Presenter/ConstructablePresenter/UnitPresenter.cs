using UnityEngine;
using System.Collections.Generic;


public class UnitPresenter : ConstructionPresenterBase<UnitModel, UnitView, UnitConstructionData>
{
    private bool isSelected = false;
    protected override void HandleClicked()
    {
        base.HandleClicked();
        Debug.Log("[UnitPresenter] Unit clicked!");
    
        isSelected = true;
        view.OnViewUpdated += Tick;

    }
    private void Tick()
    {
        if (isSelected && Input.GetMouseButtonDown(1)) 
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            MoveTo(mouseWorldPos);
         
            isSelected = false; 
        }
    }
    public void MoveTo(Vector3 targetWorldPosition)
    {
        GridCell startCell = GridManager.Instance.FindCellByGridPos(GridManager.Instance.WorldToGrid(view.transform.position));
        GridCell targetCell = GridManager.Instance.FindCellByGridPos(GridManager.Instance.WorldToGrid(targetWorldPosition));

        if (startCell == null || targetCell == null)
        {
            Debug.LogWarning("[UnitPresenter] Start or Target cell is null!");
            return;
        }
      

        if (startCell == targetCell || Vector2Int.Distance(startCell.GridPosition, targetCell.GridPosition) <= 3)
        {
            return;
        }



        List<GridCell> path = AStarPathfinder.FindPath(startCell, targetCell, out GridCell actualTarget);

        if (path != null)
        {
            startCell.IsOccupied = false;
            targetCell.IsOccupied = true;

            GridManager.Instance.SetAreaOccupied(actualTarget.GridPosition, Vector2Int.one, true);

            UnitPathFollower follower = view.GetComponent<UnitPathFollower>();
            if (follower == null)
                follower = view.gameObject.AddComponent<UnitPathFollower>();

            follower.FollowPath(path);
        }
    }

}
