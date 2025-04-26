using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridHoverGhost : MonoBehaviour
{
    public static GridHoverGhost Instance { get; private set; }

    public event System.Action<Vector3> PlacementConfirmed;


    [SerializeField] private GameObject ghostPrefab;
    private GameObject currentGhost;

    [SerializeField] private Vector2Int ghostSize = new Vector2Int(2, 2);


    private Coroutine hoverRoutine;

    private bool isActive;
    private bool canPlace;

    private float cellSize;

    private SpriteRenderer ghostRenderer;

    private readonly Color validColor = new Color(0f, 0f, 1f, 0.4f);
    private readonly Color invalidColor = new Color(1f, 0f, 0f, 0.4f);

    private Vector2Int lastGridPos = new Vector2Int(int.MinValue, int.MinValue);


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        currentGhost = Instantiate(ghostPrefab);

        ghostRenderer = currentGhost.GetComponent<SpriteRenderer>();
        currentGhost.SetActive(false);
        cellSize = GridManager.Instance.CellSize;
    }



    public void StartHover(Vector2Int _ghostSize)
    {
        if (hoverRoutine != null)
            StopCoroutine(hoverRoutine);
        ghostSize = _ghostSize;
        isActive = true;
        hoverRoutine = StartCoroutine(HoverFollowMouse());
    }

    public void CancelHover()
    {
        isActive = false;
        if (hoverRoutine != null) StopCoroutine(hoverRoutine);
        currentGhost.gameObject.SetActive(false);
    }
    private IEnumerator HoverFollowMouse()
    {

        currentGhost.SetActive(true);

        while (isActive)
        {

            // 1. Convert mouse position to world coordinates
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            // 2. Calculate the grid position
            Vector2Int gridPos = GridManager.Instance.WorldToGrid(mouseWorld);
            // if (gridPos != lastGridPos)
            {
                lastGridPos = gridPos;


                Vector2Int startPos = gridPos + new Vector2Int(ghostSize.x / 2, ghostSize.y / 2);

                // 3.  Check if the location is valid
                canPlace = GridManager.Instance.CanPlaceBuildingAt(gridPos, ghostSize);

                // 4. Calculate offset to center the object
                Vector2 offset = new Vector2(
                    (ghostSize.x % 2 != 0) ? 0.5f : 0f,
                    (ghostSize.y % 2 != 0) ? 0.5f : 0f
                );

                // 5. Calculate the spawn position
                Vector3 spawnPos = GridManager.Instance.GridToWorld(startPos)
                                 + new Vector3(offset.x, offset.y) * cellSize;

                // 6. Set the ghost's position and scale
                currentGhost.transform.position = spawnPos;
                currentGhost.transform.localScale = Vector2.one * cellSize * ghostSize;
                currentGhost.SetActive(true);

                // 7. Set the ghost color: is the placement valid?

                Color targetColor = canPlace ? validColor : invalidColor;
                if (ghostRenderer.color != targetColor)
                {
                    ghostRenderer.color = targetColor;
                }



            }
            // 8. TBroadcast event if clicked
            if (Input.GetMouseButtonDown(0))
            {
                if (canPlace)
                {
                    Vector2Int pos = gridPos + new Vector2Int(ghostSize.x / 2, ghostSize.y / 2);
                    GridManager.Instance.SetAreaOccupied(gridPos, ghostSize, true);
                    Vector2 offset = new Vector2(
                    (ghostSize.x % 2 != 0) ? 0.5f : 0f,
                    (ghostSize.y % 2 != 0) ? 0.5f : 0f);

                   
                    Vector3 spawnPos = pos
                                     + new Vector2(offset.x, offset.y) * cellSize;

                    PlacementConfirmed?.Invoke(GridManager.Instance.GridToWorld(new Vector2((spawnPos.x), (spawnPos.y))));
                    CancelHover();
                    yield break;
                }
               
            }

            yield return null;
        }
    }
}
