
using System.Collections.Generic;
using UnityEngine;

public static class AStarPathfinder
{
    public static List<GridCell> FindPath(GridCell start, GridCell end, out GridCell actualTarget)
    {
        actualTarget = end;

        if (end.IsOccupied)
        {
            GridCell fallback = FindNearestUnoccupiedCell(start, end);
            if (fallback != null)
            {
                Debug.Log("[A*] Target occupied. Redirecting to fallback at " + fallback.GridPosition);
                actualTarget = fallback;
            }
            else
            {
                Debug.LogWarning("[A*] No fallback found!");
            }
        }

        // Þimdi klasik FindPath baþlasýn, actualTarget'a göre
        List<GridCell> openList = new List<GridCell>();
        HashSet<GridCell> closedList = new HashSet<GridCell>();

        openList.Add(start);

        start.GCost = 0;
        start.HCost = GetDistance(start.GridPosition, actualTarget.GridPosition);
        start.Parent = null;

        while (openList.Count > 0)
        {
            GridCell current = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < current.FCost ||
                   (openList[i].FCost == current.FCost && openList[i].HCost < current.HCost))
                {
                    current = openList[i];
                }
            }

            openList.Remove(current);
            closedList.Add(current);

            if (current == actualTarget)
            {
                return RetracePath(start, actualTarget);
            }

            foreach (GridCell neighbor in GetNeighbors(current))
            {
                if ((neighbor.IsOccupied && neighbor != actualTarget) || closedList.Contains(neighbor))
                    continue;

                int moveCostToNeighbor = current.GCost + GetDistance(current.GridPosition, neighbor.GridPosition);
                if (moveCostToNeighbor < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = moveCostToNeighbor;
                    neighbor.HCost = GetDistance(neighbor.GridPosition, actualTarget.GridPosition);
                    neighbor.Parent = current;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        return null;
    }


    private static List<GridCell> RetracePath(GridCell startCell, GridCell endCell)
    {
        List<GridCell> path = new List<GridCell>();
        GridCell current = endCell;

        while (current != startCell)
        {
            path.Add(current);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }


    private static int GetDistance(Vector2Int a, Vector2Int b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        return (dx > dy) ? 14 * dy + 10 * (dx - dy) : 14 * dx + 10 * (dy - dx);
    }

    private static List<GridCell> GetNeighbors(GridCell cell)
    {
        List<GridCell> neighbors = new List<GridCell>();
        Dictionary<Vector2, GridCell> grid = GridManager.Instance.Grid;

        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, -1)
        };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int checkPos = cell.GridPosition + dir;
            if (grid.TryGetValue(checkPos, out GridCell neighbor))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
    public static GridCell FindNearestUnoccupiedCell(GridCell from, GridCell around)
    {
        Dictionary<Vector2, GridCell> grid = GridManager.Instance.Grid;
        int maxRadius = 5; // define the maximum number of cells we will look ahead

        for (int radius = 1; radius <= maxRadius; radius++)
        {
            List<GridCell> candidates = new List<GridCell>();

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (Mathf.Abs(x) != radius && Mathf.Abs(y) != radius)
                        continue; // Only check the edges

                    Vector2Int checkPos = around.GridPosition + new Vector2Int(x, y);

                    if (grid.TryGetValue(checkPos, out GridCell neighbor))
                    {
                        if (!neighbor.IsOccupied)
                        {
                            candidates.Add(neighbor);
                        }
                    }
                }
            }

            if (candidates.Count > 0)
            {
                // select the closest
                candidates.Sort((a, b) =>
                    GetDistance(from.GridPosition, a.GridPosition).CompareTo(
                    GetDistance(from.GridPosition, b.GridPosition)));

                return candidates[0];
            }
        }

        Debug.LogWarning("[A*] No unoccupied neighbor found after expanding search!");
        return null;
    }

}
