using UnityEngine;

public class GridCell
{
    public Vector2Int GridPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public bool IsOccupied { get; set; }

    //---------------------------------------------------
    //                 A* PATHFINDING SETTINGS
    //---------------------------------------------------
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost => GCost + HCost;
    public GridCell Parent { get; set; }
    //---------------------------------------------------
    //                 A* PATHFINDING SETTINGS
    //---------------------------------------------------

    public GridCell(Vector2Int gridPos, Vector3 worldPos)
    {
        GridPosition = gridPos;
        WorldPosition = worldPos;
        IsOccupied = false;
    }
}
