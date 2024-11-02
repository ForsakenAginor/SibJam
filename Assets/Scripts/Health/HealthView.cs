using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private Image _image;
    [SerializeField] private Color _rageColor;
    [SerializeField] private Color _hopelessnessColor;
    [SerializeField] private Color _fearColor;
    [SerializeField] private Color _averageColors;
    [SerializeField] private Color _hapinessColors;

    private Dictionary<Health, Color> _colors = new Dictionary<Health, Color>();

    private void Awake()
    {
        _colors.Add(Health.Rage, _rageColor);
        _colors.Add(Health.Hopeless, _hopelessnessColor);
        _colors.Add(Health.Fear, _fearColor);
        _colors.Add(Health.Average, _averageColors);
        _colors.Add(Health.Happiness, _hapinessColors);
        _colors.Add(Health.Riot, _rageColor);
    }

    public void Init(Health health)
    {
        _image.color = _colors[health];
        _textField.text = health.ToString();
    }
}
