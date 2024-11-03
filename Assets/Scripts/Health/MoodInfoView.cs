using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoodInfoView : MonoBehaviour
{
    [SerializeField] private UIElement _infoWindow;
    [SerializeField] private TMP_Text _textField;

    private readonly Dictionary<Health, string> _moodDescriptions = new Dictionary<Health, string>()
    {
        {Health.Happiness, "1"},
        {Health.Average, "2"},
        {Health.Fear, "3"},
        {Health.Hopeless, "4"},
        {Health.Rage, "5"},
    };

    public void ShowMoodInfo(Health mood)
    {
        _textField.text = _moodDescriptions[mood];
        _infoWindow.Enable();
    }
}
