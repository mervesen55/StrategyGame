//using System;
//using UnityEngine;

//public class BuildingButtonFactory : MonoBehaviour
//{
//    [SerializeField] private Transform buttonParent;
//    [SerializeField] private GameObject buttonPrefab; // içinde BuildingButtonView var

//    private void Start()
//    {
//        foreach (BuildingType type in Enum.GetValues(typeof(BuildingType)))
//        {
//            GameObject go = Instantiate(buttonPrefab, buttonParent);
//            var view = go.GetComponent<BuildingButtonView>();
//            var presenter = new BuildingButtonPresenter();
//            presenter.Init(type, view);


//        }
//    }
//}
