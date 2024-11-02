using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NewQuestController))]
public class NewQuestView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _header;

    private NewQuestController _controller;

    private void Awake()
    {
        _controller = GetComponent<NewQuestController>();
    }

    public void Init(Sprite sprite, string name, string description, Action<IKey> accept, Action<IKey> decline)
    {
        if (sprite == null)
            throw new ArgumentNullException(nameof(sprite));

        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException(nameof(description));

        _image.sprite = sprite;
        _header.text = name;
        _description.text = description;
        _controller.Init(accept, decline);
    }

    public IKey GetKey() => _controller;
}