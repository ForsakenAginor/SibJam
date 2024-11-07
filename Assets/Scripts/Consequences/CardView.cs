using Assets.Scripts.General;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Consequences
{
    [RequireComponent(typeof(CardController))]
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _header;

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
            _header.text = name;
            _description.text = description;
            _controller.Init(audioSource, callback);
        }

        public IKey GetKey()
        {
            return _controller;
        }
    }
}