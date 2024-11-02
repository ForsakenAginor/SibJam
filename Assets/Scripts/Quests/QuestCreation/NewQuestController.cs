using System;
using UnityEngine;
using UnityEngine.UI;

public class NewQuestController : MonoBehaviour, IKey
{
    [SerializeField] private Button _toBoardButton;
    [SerializeField] private Button _toTableButton;

    private AudioSource _audioSource;

    private Action<IKey> Accept;
    private Action<IKey> Decline;

    private void OnDestroy()
    {
        _toBoardButton.onClick.RemoveListener(OnToBoardButtonClick);
        _toTableButton.onClick.RemoveListener(OnToTableButtonClick);
    }

    public void Init(AudioSource audioSource, Action<IKey> accept, Action<IKey> decline)
    {
        _audioSource = audioSource != null ? audioSource : throw new ArgumentNullException(nameof(audioSource));
        Accept = accept;
        Decline = decline;
        _toBoardButton.onClick.AddListener(OnToBoardButtonClick);
        _toTableButton.onClick.AddListener(OnToTableButtonClick);
    }

    private void OnToTableButtonClick()
    {
        _audioSource.Play();
        Decline?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnToBoardButtonClick()
    {
        _audioSource.Play();
        Accept?.Invoke(this);
        gameObject.SetActive(false);
    }
}