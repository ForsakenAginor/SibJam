using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mood
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;
        [SerializeField] private Image _image;
        [SerializeField] private Color _rageColor;
        [SerializeField] private Color _hopelessnessColor;
        [SerializeField] private Color _fearColor;
        [SerializeField] private Color _averageColors;
        [SerializeField] private Color _hapinessColors;

        private Dictionary<Health, KeyValuePair<Color, string>> _colors = new Dictionary<Health, KeyValuePair<Color, string>>();

        private void Awake()
        {
            _colors.Add(Health.Rage, new KeyValuePair<Color, string>(_rageColor, "Ярость"));
            _colors.Add(Health.Hopeless, new KeyValuePair<Color, string>(_hopelessnessColor, "Гнев"));
            _colors.Add(Health.Fear, new KeyValuePair<Color, string>(_fearColor, "Досада"));
            _colors.Add(Health.Average, new KeyValuePair<Color, string>(_averageColors, "Безразличие"));
            _colors.Add(Health.Happiness, new KeyValuePair<Color, string>(_hapinessColors, "Счастье"));
            _colors.Add(Health.Riot, new KeyValuePair<Color, string>(_rageColor, "Бунт"));
        }

        public void Init(Health health)
        {
            _image.color = _colors[health].Key;
            _textField.text = _colors[health].Value;
        }
    }
}