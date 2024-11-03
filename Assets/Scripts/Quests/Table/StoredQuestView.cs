using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIElement))]
public class StoredQuestView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_Text _daysRemaining;

    private UIElement _uIElement;

    private void Awake()
    {
        _uIElement = GetComponent<UIElement>();
    }

    public void Init(Sprite sprite, string name, string description, int daysToExpire)
    {
        if (sprite == null)
            throw new ArgumentNullException(nameof(sprite));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException(nameof(description));

        if(daysToExpire <= 0)
            throw new ArgumentOutOfRangeException(nameof(daysToExpire));

        //_image.sprite = sprite;
        _header.text = name;
        _description.text = description;
        _daysRemaining.text = daysToExpire.ToString();
    }

    public UIElement GetUIElement() => _uIElement;
}
