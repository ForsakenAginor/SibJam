using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IKey
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _image;

    private UIElement _card;
    private AudioSource _audioSource;

    private Action<IKey> Chose;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _card.Enable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _card.Disable();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _audioSource.Play();
        Chose?.Invoke(this);
        _card.Disable();
        Destroy(_card.gameObject);
        Destroy(gameObject);
    }

    private void Awake()
    {
        int index = UnityEngine.Random.Range(0, _sprites.Count);
        _image.sprite = _sprites[index];
    }

    public void Init(UIElement card, AudioSource audio, Action<IKey> accept)
    {
        _card = card != null ? card : throw new ArgumentNullException(nameof(card));
        _audioSource = audio != null ? audio : throw new ArgumentNullException(nameof(audio));
        Chose = accept;
    }
}
