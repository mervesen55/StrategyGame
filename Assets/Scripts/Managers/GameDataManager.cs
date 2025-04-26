using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    //public static GameDataManager Instance { get; private set; }

    //public List<BuildingButtonData> allBuildings;


    //[SerializeField] private List<BaseProductData> allProductDataList;

    //private Dictionary<ProductType, BaseProductData> productDataMap;

    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    Instance = this;


      
    //    InitProductMap();
    //}

    //private void InitProductMap()
    //{
    //    productDataMap = new Dictionary<ProductType, BaseProductData>();
    //    foreach (var data in allProductDataList)
    //    {
    //        if (!productDataMap.ContainsKey(data.productType))
    //            productDataMap[data.productType] = data;
    //        else
    //            Debug.LogWarning($"Duplicate ProductType: {data.productType} in GameDataManager.");
    //    }
    //}

    //public BaseProductData GetProductData(ProductType type)
    //{
    //    return productDataMap.TryGetValue(type, out var data) ? data : null;
    //}

    //public List<BuildingButtonData> GetAllBuildingButtons()
    //{
    //    List<BuildingButtonData> result = new();
    //    foreach (var data in allProductDataList)
    //    {
    //        if (data is BuildingButtonData buttonData)
    //            result.Add(buttonData);
    //    }
    //    return result;
    //}

    //public List<UnitButtonData> GetAllUnitButtons()
    //{
    //    List<UnitButtonData> result = new();
    //    foreach (var data in allProductDataList)
    //    {
    //        if (data is UnitButtonData buttonData)
    //            result.Add(buttonData);
    //    }
    //    return result;
    //}

}
