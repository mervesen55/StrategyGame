using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public GameObject GetFromPool(string prefabName)
    {
        if (poolDictionary.ContainsKey(prefabName) && poolDictionary[prefabName].Count > 0)
        {
            GameObject obj = poolDictionary[prefabName].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        string prefabName = obj.name.Replace("(Clone)", "").Trim();

        if (!poolDictionary.ContainsKey(prefabName))
        {
            poolDictionary[prefabName] = new Queue<GameObject>();
        }

        obj.SetActive(false);
        poolDictionary[prefabName].Enqueue(obj);
    }
}
