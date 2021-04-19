using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    public Canvas canvas;
    public CanvasGroup canvasgroup;
    public GameObject Mask;

    private void Awake()
    {
        
        rect = GetComponent<RectTransform>();

        canvasgroup = rect.gameObject.GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
        canvasgroup.alpha = 0.7f;
        canvasgroup.blocksRaycasts = false;
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
        canvasgroup.alpha = 1f;
        canvasgroup.blocksRaycasts = false;
        CharacterManager.colorCount++;
        if (CharacterManager.colorCount == 2)
        {
            CharacterManager.next = true;
        }

        if (CharacterManager.colorCount == 6)
        {
            CharacterManager.next = true;
        }

        if (CharacterManager.colorCount == 10)
        {
            CharacterManager.next = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("down drag");
    }
}