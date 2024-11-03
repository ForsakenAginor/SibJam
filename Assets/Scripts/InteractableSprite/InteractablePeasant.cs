using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class InteractablePeasant : MonoBehaviour, IPointerDownHandler
{
    private const string MaterialEnablePropertyName = "_IsEnable";

    [SerializeField] private float _animationDuration;
    [SerializeField] private float _stepFrequence;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _stepValue;

    private SpriteRenderer _spriteRenderer;
    private UIElement _windowThatWillBeOpened;
    private Material _material;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = new Material(_spriteRenderer.material);
        _spriteRenderer.material = _material;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_windowThatWillBeOpened == null)
            return;

        _material.SetInt(MaterialEnablePropertyName, 0);
        _windowThatWillBeOpened.Enable();
        _windowThatWillBeOpened = null;

        _spriteRenderer.flipX = true;
        Vector3 target = new Vector3(_targetPoint.position.x, transform.position.y, transform.position.z);
        transform.DOMove(target, _animationDuration).SetEase(Ease.Linear);
        _stepValue += transform.position.y;
        transform.DOMoveY(_stepValue, _stepFrequence).SetLoops(-1, LoopType.Yoyo);
    }

    public void Init(UIElement uIElement)
    {
        _windowThatWillBeOpened = uIElement;
    }
}
