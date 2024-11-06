using Assets.Scripts.General;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IKey
{
    [SerializeField] private Button _button;

    private AudioSource _audioSource;

    private Action<IKey> ButtonClick;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(AudioSource audioSource, Action<IKey> callback)
    {
        _audioSource = audioSource != null ? audioSource : throw new ArgumentNullException(nameof(audioSource));
        ButtonClick = callback;
    }


    private void OnButtonClick()
    {
        _audioSource.Play();
        ButtonClick?.Invoke(this);
        gameObject.SetActive(false);
    }
}
