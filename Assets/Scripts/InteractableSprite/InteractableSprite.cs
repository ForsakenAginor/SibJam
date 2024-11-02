using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class InteractableSprite : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UIElement _windowThatWillBeOpened;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
        _windowThatWillBeOpened.Enable();
    }

    protected void SetTargetWindow(UIElement uIElement)
    {
        _windowThatWillBeOpened = uIElement;
    }
}
