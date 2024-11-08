using Assets.Scripts.UI;
using Lean.Localization;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Quests.Storage.View
{
    [RequireComponent(typeof(UIElement))]
    public class StoredQuestView : MonoBehaviour
    {
        [SerializeField] private LeanLocalizedTextMeshProUGUI _description;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _header;
        [SerializeField] private TMP_Text _daysRemaining;

        private UIElement _uIElement;

        private void Awake()
        {
            _uIElement = GetComponent<UIElement>();
        }

        public void Init(string name, string description, int daysToExpire)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));

            if (daysToExpire <= 0)
                throw new ArgumentOutOfRangeException(nameof(daysToExpire));

            _header.TranslationName = name;
            _header.UpdateLocalization();
            _description.TranslationName = description;
            _description.UpdateLocalization();
            _daysRemaining.text = daysToExpire.ToString();
        }

        public UIElement GetUIElement() => _uIElement;
    }
}