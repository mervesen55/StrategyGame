using UnityEngine;

public abstract class BaseProductData : ScriptableObject
{
    public ProductType productType;
    public GameObject prefab;
    public GameObject productPrefab;
    public Sprite icon;
    public Vector2Int dimension;
    public string displayName;
}
