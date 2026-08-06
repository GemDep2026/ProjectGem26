using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverArrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ArrowSelector arrowSelector;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        arrowSelector.ShowArrow(rect);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        arrowSelector.HideArrow();
    }
}