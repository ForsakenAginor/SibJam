using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IKey
{
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

    public void Init(UIElement card, AudioSource audio, Action<IKey> accept)
    {
        _card = card != null ? card : throw new ArgumentNullException(nameof(card));
        _audioSource = audio != null ? audio : throw new ArgumentNullException(nameof(audio));
        Chose = accept;
    }
}
