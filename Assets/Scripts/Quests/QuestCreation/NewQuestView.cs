using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIElement))]
[RequireComponent(typeof(NewQuestController))]
public class NewQuestView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _header;

    private NewQuestController _controller;
    private UIElement _uiElement;

    private void Awake()
    {
        _controller = GetComponent<NewQuestController>();
        _uiElement = GetComponent<UIElement>();
    }

    public void Init(Sprite sprite, string name, string description,
        AudioSource toDeskSound, AudioSource toBagSound,
        Action<IKey> accept, Action<IKey> decline)
    {
        if (sprite == null)
            throw new ArgumentNullException(nameof(sprite));

        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException(nameof(description));

        if (toDeskSound == null)
            throw new ArgumentNullException(nameof(toDeskSound));

        if (toBagSound == null)
            throw new ArgumentNullException(nameof(toBagSound));

        //_image.sprite = sprite;
        _header.text = name;
        _description.text = description;
        _controller.Init(toDeskSound, toBagSound, accept, decline);
    }

    public IKey GetKey() => _controller;

    public UIElement GetUIElement() => _uiElement;
}