using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using Unity.Burst.CompilerServices;

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector2 startPosition = new Vector2(0, -140);

    [SerializeField] private float outOfBoundsThreshold = 40f;

    [SerializeField] private float childHeight = 125f;
    [SerializeField] private float itemSpacing = 30f;


    Vector2 lastDragPosition;

    bool positiveDrag;

    int childCount = 0;


    float height = 0f;



    IEnumerator  Start()
    {
       
        yield return new WaitForSeconds(0.5f);
        // Allow the ScrollRect to move freely without clamping
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

        // Cache total child count (row containers)
        childCount = scrollRect.content.childCount;

        // Cache screen height (can be used for scroll boundaries)
        height = Screen.height;


    }


    private void OnEnable()
    {
        scrollRect.onValueChanged.AddListener(HandleScrollRectValueChanged);
    }

    private void OnDisable()
    {
        scrollRect.onValueChanged.RemoveListener(HandleScrollRectValueChanged);
    }

    private void HandleScrollRectValueChanged(Vector2 arg0)
    {
        int currentItemIndex = positiveDrag ? childCount - 1 : 0;

        var currentItem = scrollRect.content.GetChild(currentItemIndex);

        if (!ReachedThreshold(currentItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newposition = endItem.position;

        if (positiveDrag)
        {
            newposition.y = endItem.position.y - ((childHeight * 1.5f) + itemSpacing);
        }
        else
        {
            newposition.y = endItem.position.y + (childHeight * 1.5f) + itemSpacing;
        }

        currentItem.position = newposition;
        currentItem.SetSiblingIndex(endItemIndex);

    }


    private bool ReachedThreshold(Transform item)
    {
        float positiveYThreshold = _transform.position.y + (height * 0.5f) + outOfBoundsThreshold;
        float negativeYThreshold = _transform.position.y - (height * 0.5f) - outOfBoundsThreshold;
        return positiveDrag ?
            item.position.y - childHeight * 0.5f > positiveYThreshold :
            item.position.y + childHeight * 0.5f < negativeYThreshold;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = eventData.position;
        positiveDrag = newPosition.y > lastDragPosition.y;
        lastDragPosition = newPosition;
    }


}
