using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    public Canvas canvas;
    public GameObject Mask;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
        rect.localScale = Vector3.one * 1.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("on drag");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        gameObject.transform.parent = Mask.transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("down drag");
    }
}