using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //To Do: turn this class into DataManager
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    #region Public Properties
    public InfoPanelManager InfoPanelManager => infoPanelManager;
    public Transform ButtonContainer => buttonContainer;
    public Transform InfoPanel => infoPanel;

    public Vector2Int CurrentSpawnStartPoint { get; private set; }
    public Dictionary<UnitType, UnitButtonData> UnitButtonDataMap => unitButtonDataMap;
    #endregion

    #region Serialized Fields - Scriptable Data
    [Header("Scriptable Data")]
    [SerializeField] List<BuildingConstructionData> buildingConstructionDatas;

    [Header("Scriptable Data")]
    [SerializeField] private List<UnitButtonData> allUnitButtons;

    [Header("Scriptable Data")]
    [SerializeField] private List<BuildingButtonData> allBuildingButtos;
    #endregion

    #region Serialized Fields - UI References
    [Header("UI References")]
    [SerializeField] private Transform buttonContainer;
    [Header("UI References")]
    [SerializeField] private Transform infoPanel;

    [SerializeField] private InfoPanelManager infoPanelManager;
    #endregion

    #region Private Data Maps
    private Dictionary<BuildingType, BuildingConstructionData> buildingConstructionDataMap;

    private Dictionary<UnitType, UnitButtonData> unitButtonDataMap;

    private Dictionary<BuildingType, BuildingButtonData> buildingButtonMap;
    #endregion


    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion
        
        InitConstructionDataMap();
        InitBuildingButtonMap();
        InitUnitButtonData();
    }
    private void Start()
    {
        SpawnAllBuildingButtons(buttonContainer);
    }


    public BuildingConstructionData GetBuildingConstructionData(BuildingType type)
    {
        return buildingConstructionDataMap.TryGetValue(type, out var data) ? data : null;
    }

    public BuildingButtonData GetBuildingButtonData(BuildingType type)
    {
        return buildingButtonMap.TryGetValue(type, out var data) ? data : null;
    }

    public UnitButtonData GetUnitButtonData(UnitType type)
    {
        return unitButtonDataMap.TryGetValue(type, out var data) ? data : null;
    }

    public void SetSpawnStartPoint(Vector2Int spawnStartPoint)
    {
        CurrentSpawnStartPoint = spawnStartPoint;
    }

    private void SpawnAllBuildingButtons(Transform uiParent)
    {
        foreach (BuildingButtonData buttonData in allBuildingButtos)
        {
            if (buttonData == null || buttonData.prefab == null)
            {
                Debug.LogWarning("[GameManager] Skipped null button data or prefab.");
                continue;
            }

            GameObject go = GenericFactory.Instance.CreateProduct(buttonData.prefab, Vector3.zero);
            if (go == null)
            {
                Debug.LogError($"[GameManager] Failed to instantiate prefab via factory: {buttonData.prefab.name}");
                continue;
            }

            var view = go.GetComponent<BuildingButtonView>();
            if (view == null)
            {
                Debug.LogError($"[GameManager] Missing BuildingButtonView on prefab: {buttonData.prefab.name}");
                continue;
            }
            go.transform.SetParent(uiParent);
            var presenter = new BuildingButtonPresenter();
            presenter.Init(buttonData, view);
        }
    }

    private void InitConstructionDataMap()
    {
        buildingConstructionDataMap = new Dictionary<BuildingType, BuildingConstructionData>();

        foreach (var data in buildingConstructionDatas)
        {
            if (!buildingConstructionDataMap.ContainsKey(data.buildingType))
            {
                buildingConstructionDataMap[data.buildingType] = data;
            }        
            else
                Debug.LogWarning($"[GameManager] Duplicate buildingType in buildingConstructionDatas: {data.buildingType}");
        }
    }

    private void InitUnitButtonData()
    {
        unitButtonDataMap = new Dictionary<UnitType, UnitButtonData>();

        foreach (var data in allUnitButtons)
        {
            if (!unitButtonDataMap.ContainsKey(data.unitType))
                unitButtonDataMap[data.unitType] = data;
            else
                Debug.LogWarning($"[GameManager] Duplicate SoldierType: {data.unitType}");
        }
    }

    private void InitBuildingButtonMap()
    {
        buildingButtonMap = new Dictionary<BuildingType, BuildingButtonData>();

        foreach (var data in allBuildingButtos)
        {
            if (!buildingButtonMap.ContainsKey(data.buildingType))
                buildingButtonMap[data.buildingType] = data;
            else
                Debug.LogWarning($"[GameManager] Duplicate buildingType in allBuildings: {data.buildingType}");
        }
    }

}
