using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtonSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _hoverSize = new Vector3(0.95f, 0.95f, 0.95f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _hoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}