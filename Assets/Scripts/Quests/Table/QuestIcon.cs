using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IKey
{
    private UIElement _card;

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
        Chose?.Invoke(this);
        _card.Disable();
        gameObject.SetActive(false);
    }

    public void Init(UIElement card, Action<IKey> accept)
    {
        _card = card != null ? card : throw new ArgumentNullException(nameof(card));
        Chose = accept;
    }
}
