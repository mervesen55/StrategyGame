using UnityEngine;

public class BuildingButtonPresenter : ButtonPresenterBase<BuildingButtonModel, BuildingButtonView, BuildingButtonData>
{
    protected override void HandleClick()
    {
        GridHoverGhost.Instance.StartHover(model.Dimension);
        Debug.Log("building button presenter");
        GridHoverGhost.Instance.PlacementConfirmed += HandlePlacementConfirmed;
        //UnityEngine.Object.Instantiate(model.Data.Prefab);
    }
    private void HandlePlacementConfirmed(Vector3 pos)
    {             
        GameObject newBuilding = GenericFactory.Instance.CreateProduct(model.Data.productPrefab, pos);
        //mvp 
        BuildingView view = newBuilding.GetComponent<BuildingView>();
        BuildingPresenter presenter = new BuildingPresenter();
        BuildingConstructionData productData = GameManager.Instance.GetBuildingConstructionData(model.Data.buildingType);
        if (productData == null)
        {
            Debug.LogError($"[BuildingButtonPresenter] productData is NULL for: {model.Data.productType}");
            return;
        }
        presenter.Init(productData, view);
        // Unsubscribe:
        GridHoverGhost.Instance.PlacementConfirmed -= HandlePlacementConfirmed;
    }
}