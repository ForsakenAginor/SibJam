using Assets.Scripts.General;
using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Consequences
{
    [RequireComponent(typeof(CardController))]
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _description;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _header;

        private CardController _controller;

        private void Awake()
        {
            _controller = GetComponent<CardController>();
        }

        public void Init(Sprite sprite, string name, string description, AudioSource audioSource, Action<IKey> callback)
        {
            if (sprite == null)
                throw new ArgumentNullException(nameof(sprite));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));

            if (audioSource == null)
                throw new ArgumentNullException(nameof(audioSource));

            _image.sprite = sprite;
            _header.TranslationName = name;
            _header.UpdateLocalization();
            _description.TranslationName = description;
            _description.UpdateLocalization();
            _controller.Init(audioSource, callback);
        }

        public IKey GetKey()
        {
            return _controller;
        }
    }
}