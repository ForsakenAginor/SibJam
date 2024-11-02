using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class InteractableSprite : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UIElement _windowThatWillBeOpened;
    [SerializeField] private AudioSource _audioSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        _audioSource.Play();
        _windowThatWillBeOpened.Enable();
    }

    protected void SetTargetWindow(UIElement uIElement)
    {
        _windowThatWillBeOpened = uIElement;
    }
}
