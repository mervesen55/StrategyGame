using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class InfoPanelManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Transform unitButtonContainer; 

    private List<GameObject> spawnedUnitButtons = new List<GameObject>();

    private BuildingPresenter currentActivePresenter;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týklama
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // UI deðilse
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics2D.Raycast(ray.origin, ray.direction).collider == null)
                {                    
                    Hide();
                }
            }
        }
    }


    public void ShowInfo(ConstructionProductData constructionProductData)
    {
        if(!gameObject.activeSelf) gameObject.SetActive(true);
        if (constructionProductData == null)
        {
            Debug.LogError("[InfoPanelManager] Building data is NULL");
            return;
        }

        nameText.text = constructionProductData.displayName;
        iconImage.sprite = constructionProductData.icon;
    }

    public void ClearUnitButtons()
    {
        foreach (var button in spawnedUnitButtons)
        {
            ObjectPooler.Instance.ReturnToPool(button);
        }
        spawnedUnitButtons.Clear();
    }

    public void SpawnAllUnitButtons(Vector2Int spawnStartPoint, BuildingPresenter newPresenter)
    {
        if (currentActivePresenter == newPresenter)
            return;
        ClearUnitButtons();
        currentActivePresenter = newPresenter;
        GameManager.Instance.SetSpawnStartPoint(spawnStartPoint);

        foreach (var unitData in GameManager.Instance.UnitButtonDataMap.Values)
        {
            GameObject buttonGO = GenericFactory.Instance.CreateProduct(unitData.prefab, Vector3.zero);
            buttonGO.transform.SetParent(unitButtonContainer, false);

        
            var view = buttonGO.GetComponent<UnitButtonView>();
            if (!view.isInitilazed)
            {
                UnitButtonPresenter presenter = new UnitButtonPresenter();
                presenter.Init(unitData, view);
            }
            

            spawnedUnitButtons.Add(buttonGO);
        }
    }

    private void Hide()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }

}
