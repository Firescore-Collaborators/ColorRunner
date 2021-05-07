using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    public Canvas canvas;
    public CanvasGroup canvasgroup;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
        canvasgroup.alpha = 0.5f;
        canvasgroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("on drag");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        canvasgroup.blocksRaycasts = true;
        canvasgroup.alpha = 1f;
        CharacterManager.colorCount++;

        if (CharacterManager.colorCount == 5)
        {
            CharacterManager.next = true;
        }

        if (CharacterManager.colorCount == 9)
        {
            CharacterManager.next = true;
        }

        if (CharacterManager.colorCount == 12)
        {
            CharacterManager.next = true;
        }
    }

 

    public void OnPointerDown(PointerEventData eventData)
    {
        print("down drag");
    }
}
