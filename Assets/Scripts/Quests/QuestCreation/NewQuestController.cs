using System;
using UnityEngine;
using UnityEngine.UI;

public class NewQuestController : MonoBehaviour, IKey
{
    [SerializeField] private Button _toBoardButton;
    [SerializeField] private Button _toTableButton;

    private Action<IKey> Accept;
    private Action<IKey> Decline;

    private void OnDestroy()
    {
        _toBoardButton.onClick.RemoveListener(OnToBoardButtonClick);
        _toTableButton.onClick.RemoveListener(OnToTableButtonClick);
    }

    public void Init(Action<IKey> accept, Action<IKey> decline)
    {
        Accept = accept;
        Decline = decline;
        _toBoardButton.onClick.AddListener(OnToBoardButtonClick);
        _toTableButton.onClick.AddListener(OnToTableButtonClick);
    }

    private void OnToTableButtonClick()
    {
        Decline?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnToBoardButtonClick()
    {
        Accept?.Invoke(this);
        gameObject.SetActive(false);
    }
}