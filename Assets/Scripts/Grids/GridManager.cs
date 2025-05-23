using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Singleton
    public static GridManager Instance;
    #endregion

    #region Properties
    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;
    public Vector3 OriginPosition => originPosition;

    public Dictionary<Vector2, GridCell> Grid => grid;

    #endregion


    [Header("Grid Settings")]
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;

    [SerializeField] private float cellSize = 1f;

    [SerializeField] private Vector3 originPosition = Vector3.zero;

    [SerializeField] private GameObject tilePrefab;

    private Dictionary<Vector2, GridCell> grid = new Dictionary<Vector2, GridCell>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        GenerateGrid();
    }

    private void OnDrawGizmos()
    {
        if (grid == null || grid.Count == 0) return;

        Gizmos.color = Color.green;

        foreach (var cell in grid.Values)
        {
            Vector3 center = cell.WorldPosition + new Vector3(cellSize, cellSize, 0f) * 0.5f;
            Gizmos.DrawWireCube(center, new Vector3(cellSize, cellSize, 0.01f)); // ince �izim Z y�n�nde
            Vector3 labelPos = cell.WorldPosition + Vector3.one * (cellSize * 0.5f);
            UnityEditor.Handles.color = Color.white;
            UnityEditor.Handles.Label(labelPos, cell.GridPosition.ToString());
        }
    }


    private void GenerateGrid()
    {
        grid.Clear();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);

                Vector3 worldPos = originPosition
                                 + new Vector3(
                                     (x - width / 2f) * cellSize,
                                     (y - height / 2f) * cellSize,
                                     0f);

                grid[gridPos] = new GridCell(gridPos, worldPos);
            }
        }
     
    }
    private void SpawnVisualTiles()
    {
        foreach (var cell in grid.Values)
        {
            Vector3 worldPos = cell.WorldPosition;

            GameObject tile = Instantiate(tilePrefab, worldPos, Quaternion.identity, this.transform);

            tile.transform.position = new Vector3(worldPos.x + tile.transform.localScale.x / 2, worldPos.y + tile.transform.localScale.x / 2, 1f);

            tile.transform.localScale = Vector3.one * cellSize;
        }
    }


    public Vector2Int WorldToGrid(Vector3 worldPos)
    {

        float adjustedX = ((worldPos.x - originPosition.x) / cellSize) + (width / 2f);
        float adjustedY = ((worldPos.y - originPosition.y) / cellSize) + (height / 2f);

        int x = Mathf.FloorToInt(adjustedX);
        int y = Mathf.FloorToInt(adjustedY);

        return new Vector2Int(x, y);
    }
    public Vector3 GridToWorld(Vector2 gridPos)
    {
        return originPosition
            + new Vector3(
                (gridPos.x - width / 2f) * cellSize,
                (gridPos.y - height / 2f) * cellSize,
                0f);
    }
    public bool CanPlaceBuildingAt(Vector2Int startPos, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int pos = startPos + new Vector2Int(x, y);

                if (!IsInsideGrid(pos))
                    return false;


                if (grid[pos].IsOccupied)
                    return false;
            }
        }

        return true;
    }
    public bool IsInsideGrid(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }
    public void SetAreaOccupied(Vector2 origin, Vector2Int size, bool occupied)
    {

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2 pos = origin + new Vector2(x, y);

                if (grid.ContainsKey(pos))
                    grid[pos].IsOccupied = occupied;
            }
        }
    }

    public GridCell FindCellByGridPos(Vector2Int gridPos)
    {
        if (grid.TryGetValue(gridPos, out var cell))
            return cell;

        return null;
    }

    public GridCell FindAvailableSpawnPoint(Vector2Int startingGrid, Vector2Int dimension)
    {      
        int bottomCellCount = dimension.x + 2;

        Vector2Int cell = startingGrid;
        Vector2Int lastCell = new Vector2Int();

        //bottom cells
        for (int i = 0; i < bottomCellCount; i++)
        {
            cell = startingGrid + Vector2Int.right * i;
            lastCell = cell;
         
            if (grid.ContainsKey(cell))
            {
                if (!grid[cell].IsOccupied) 
                    return grid[cell];
               
            }
            else return null;
           
        }

        //right side cells
        for (int i = 0; i < dimension.y; i++)
        {
          
            cell = lastCell + Vector2Int.up;
            lastCell = cell;
          
            if (grid.ContainsKey(cell))
            {
                if (!grid[cell].IsOccupied) return grid[cell];
            }
            else return null;

        }

        lastCell += Vector2Int.up;
       
        if (grid.ContainsKey(lastCell) && !grid[lastCell].IsOccupied) return grid[lastCell];


        //top side cells
        for (int i = 0; i < (bottomCellCount-1); i++)
        {
            cell = lastCell - Vector2Int.right;
            lastCell = cell;
            
            if (grid.ContainsKey(cell))
            {
                if (!grid[cell].IsOccupied) return grid[cell];
            }
            else return null;

        }

        //down side cells
        for (int i = 0; i < dimension.y; i++)
        {
            cell = lastCell - Vector2Int.up;
            lastCell = cell;
     
            if (grid.ContainsKey(cell))
            {
                if (!grid[cell].IsOccupied) return grid[cell];
            }
            else return null;
        }
        Vector2Int newStartingGrid = startingGrid - Vector2Int.one;
        Vector2Int newDimension = dimension + Vector2Int.one * 2;
        return FindAvailableSpawnPoint(newStartingGrid, newDimension);
    }
   

}

