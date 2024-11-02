using System;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IKey
{
    [SerializeField] private Button _button;

    private Action<IKey> ButtonClick;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(Action<IKey> callback)
    {
        ButtonClick = callback;
    }


    private void OnButtonClick()
    {
        ButtonClick?.Invoke(this);
        gameObject.SetActive(false);
    }
}
