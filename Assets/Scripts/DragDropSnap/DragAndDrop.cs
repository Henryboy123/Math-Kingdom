using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler,
    IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    Vector2 originalTransform;

    private CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalTransform = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DragStarted");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += 
            eventData.delta/ canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DragEnded");
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = originalTransform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }
}
