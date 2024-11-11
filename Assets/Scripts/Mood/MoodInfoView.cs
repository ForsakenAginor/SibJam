using Assets.Scripts.UI;
using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mood
{
    public class MoodInfoView : MonoBehaviour
    {
        [SerializeField] private UIElement _infoWindow;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedText;

        private readonly Dictionary<Health, string> _moodDescriptions = new Dictionary<Health, string>()
        {
            {Health.Happiness, "HappinessD"},
            {Health.Average, "AverageD"},
            {Health.Fear, "FearD"},
            {Health.Hopeless, "HopelessD"},
            {Health.Rage, "RageD"},
        };

        public void ShowMoodInfo(Health mood)
        {
            _localizedText.TranslationName = _moodDescriptions[mood];
            _localizedText.UpdateLocalization();
            _infoWindow.Enable();
        }
    }
}