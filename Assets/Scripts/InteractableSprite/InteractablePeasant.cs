using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(InteractableSprite))]
[RequireComponent(typeof(SpriteRenderer))]
public class InteractablePeasant : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _stepFrequence;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _stepValue;

    private InteractableSprite _interactableSprite;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _interactableSprite = GetComponent<InteractableSprite>();
        _interactableSprite.Pressed += OnSpritePressed;
    }

    private void OnDestroy()
    {
        _interactableSprite.Pressed -= OnSpritePressed;
    }

    private void OnSpritePressed()
    {
        _spriteRenderer.flipX = true;
        Vector3 target = new Vector3(_targetPoint.position.x, transform.position.y, transform.position.z);
        transform.DOMove(target, _animationDuration).SetEase(Ease.Linear);
        _stepValue += transform.position.y;
        transform.DOMoveY(_stepValue, _stepFrequence).SetLoops(-1, LoopType.Yoyo);
    }
}
