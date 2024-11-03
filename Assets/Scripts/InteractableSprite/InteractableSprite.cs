using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class InteractableSprite : MonoBehaviour, IPointerDownHandler
{
    private const string MaterialEnablePropertyName = "_IsEnable";

    [SerializeField] private UIElement _windowThatWillBeOpened;
    [SerializeField] private AudioSource _audioSource;

    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private bool _isEnabled = true;

    public event Action Pressed;

    public  bool IsEnabled => _isEnabled;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = new Material (_spriteRenderer.material);
        _spriteRenderer.material = _material;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isEnabled == false)
            return;

        _material.SetInt(MaterialEnablePropertyName, 0);
        _audioSource.Play();
        _windowThatWillBeOpened.Enable();
        _isEnabled = false;
        Pressed?.Invoke();
    }
    
    public void BecameInteractable()
    {
        _material.SetInt(MaterialEnablePropertyName, 1);
        _isEnabled = true;
    }
}
