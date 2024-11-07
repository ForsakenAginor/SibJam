using Assets.Scripts.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.InteractableObjects
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class InteractableSprite : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private const string MaterialEnablePropertyName = "_IsEnable";

        [SerializeField] private UIElement _windowThatWillBeOpened;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Button _button;

        [Header("PulseAnimationSettings")]
        [SerializeField] private int _pulses = 5;
        [SerializeField] private float _duration = 0.2f;

        private SpriteRenderer _spriteRenderer;
        private Material _material;
        private bool _isEnabled = true;
        private Coroutine _pulseAnimation;

        public event Action Pressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_pulseAnimation != null)
                StopCoroutine(_pulseAnimation);

            if (_isEnabled == false)
                return;

            _material.SetInt(MaterialEnablePropertyName, 0);

            if (_audioSource != null)
                _audioSource.Play();

            _windowThatWillBeOpened.Enable();
            _isEnabled = false;
            Pressed?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_pulseAnimation != null)
                StopCoroutine(_pulseAnimation);

            if (_isEnabled)
                _material.SetInt(MaterialEnablePropertyName, 0);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_pulseAnimation != null)
                StopCoroutine(_pulseAnimation);

            if (_isEnabled)
                _material.SetInt(MaterialEnablePropertyName, 1);
        }

        public bool IsEnabled => _isEnabled;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = new Material(_spriteRenderer.material);
            _spriteRenderer.material = _material;
            _material.SetInt(MaterialEnablePropertyName, 0);

            if (_button != null)
                _button.onClick.AddListener(BecameInteractable);
        }

        private void OnDestroy()
        {
            if (_button != null)
                _button.onClick.RemoveListener(BecameInteractable);
        }

        public void Init(UIElement targetWindow)
        {
            _windowThatWillBeOpened = targetWindow != null ? targetWindow : throw new ArgumentNullException(nameof(targetWindow));
        }

        public void BecameInteractable()
        {
            _isEnabled = true;
        }

        public void PlayPulseAnimation()
        {
            if (_pulseAnimation != null)
                StopCoroutine(_pulseAnimation);

            _pulseAnimation = StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            WaitForSeconds delay = new WaitForSeconds(_duration);

            for (int i = 0; i < _pulses; i++)
            {
                _material.SetInt(MaterialEnablePropertyName, 1);
                yield return delay;
                _material.SetInt(MaterialEnablePropertyName, 0);
                yield return delay;
            }
        }
    }
}