using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GenericFactory : MonoBehaviour
{
    public static GenericFactory Instance { get; private set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //public GameObject CreateProduct(GameObject prefab, Vector3 position)
    //{    
    //    return Instantiate(prefab, position, Quaternion.identity);
    //}
    public GameObject CreateProduct(GameObject prefab, Vector3 position)
    {
        if (prefab == null)
        {
            Debug.LogError("[GenericFactory] Prefab is null!");
            return null;
        }

        GameObject instance = ObjectPooler.Instance.GetFromPool(prefab.name);

        if (instance == null)
        {
            instance = Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            instance.transform.position = position;
            instance.SetActive(true);
        }

        return instance;
    }


}
