using UnityEngine;
using System.Collections.Generic;

public class AStarTestTool : MonoBehaviour
{
    private GridCell startCell;
    private GridCell endCell;

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector2Int gridPos = GridManager.Instance.WorldToGrid(mouseWorldPos);
    //        startCell = GridManager.Instance.FindCellByGridPos(gridPos);
    //        Debug.Log($"Start Cell: {startCell.GridPosition}");
    //    }

    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector2Int gridPos = GridManager.Instance.WorldToGrid(mouseWorldPos);
    //        endCell = GridManager.Instance.FindCellByGridPos(gridPos);
    //        Debug.Log($"End Cell: {endCell.GridPosition}");

    //        if (startCell != null && endCell != null)
    //        {
    //            List<GridCell> path = AStarPathfinder.FindPath(startCell, endCell);


    //            if (path != null)
    //            {
    //                Debug.Log("Path:");
    //                foreach (var cell in path)
    //                {
    //                    Debug.Log(cell.GridPosition);
    //                }
    //            }
    //            else
    //            {
    //                Debug.LogWarning("No path found!");
    //            }
    //        }
    //    }
    //}
}
