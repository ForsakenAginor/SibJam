using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewQuestView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private Button _toBoardButton;
    [SerializeField] private Button _toStockButton;

    private void Awake()
    {
        _toBoardButton.onClick.AddListener(OnButtonClick);
        _toStockButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _toBoardButton.onClick.RemoveListener(OnButtonClick);
        _toStockButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(Sprite sprite, string name, string description)
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
    }

    private void OnButtonClick()
    {
        gameObject.SetActive(false);
    }
}
