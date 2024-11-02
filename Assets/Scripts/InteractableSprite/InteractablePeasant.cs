using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class InteractablePeasant : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _targetPoint;

    private UIElement _windowThatWillBeOpened;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_windowThatWillBeOpened == null)
            return;

        _windowThatWillBeOpened.Enable();
        _windowThatWillBeOpened = null;

        _spriteRenderer.flipX = true;
        Vector3 target = new Vector3(_targetPoint.position.x, transform.position.y, transform.position.z);
        transform.DOMove(target, _animationDuration).SetEase(Ease.Linear);
    }

    public void Init(UIElement uIElement)
    {
        _windowThatWillBeOpened = uIElement;
    }
}
