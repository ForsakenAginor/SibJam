using Assets.Scripts.Quests.QuestCreation.Controller;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Quests.QuestCreation.View
{
    [RequireComponent(typeof(UIElement))]
    [RequireComponent(typeof(NewQuestController))]
    public class NewQuestView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _header;

        private NewQuestController _controller;
        private UIElement _uiElement;

        private void Awake()
        {
            _controller = GetComponent<NewQuestController>();
            _uiElement = GetComponent<UIElement>();
        }

        public void Init(string name, string description,
            AudioSource toDeskSound, AudioSource toBagSound,
            Action<IKey> accept, Action<IKey> decline)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));

            if (toDeskSound == null)
                throw new ArgumentNullException(nameof(toDeskSound));

            if (toBagSound == null)
                throw new ArgumentNullException(nameof(toBagSound));

            _header.text = name;
            _description.text = description;
            _controller.Init(toDeskSound, toBagSound, accept, decline);
        }

        public IKey GetKey() => _controller;

        public UIElement GetUIElement() => _uiElement;
    }
}