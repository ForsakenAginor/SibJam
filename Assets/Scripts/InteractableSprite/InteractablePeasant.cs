using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractablePeasant : MonoBehaviour, IPointerDownHandler
{
    private UIElement _windowThatWillBeOpened;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
        _windowThatWillBeOpened?.Enable();
        _windowThatWillBeOpened = null;
    }

    public void Init(UIElement uIElement)
    {
        _windowThatWillBeOpened = uIElement;
    }
}
