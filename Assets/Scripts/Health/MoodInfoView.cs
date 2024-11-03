using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoodInfoView : MonoBehaviour
{
    [SerializeField] private UIElement _infoWindow;
    [SerializeField] private TMP_Text _textField;

    private readonly Dictionary<Health, string> _moodDescriptions = new Dictionary<Health, string>()
    {
        {Health.Happiness, "Жители деревни улыбаются и полны радости! Вы сделали их день лучше, и они благодарны вам за это"},
        {Health.Average, "Жители деревни спокойны и не испытывают ни особой радости, ни тревоги. Кажется, вы их устраиваете"},
        {Health.Fear, "Жители деревни начинают чувствовать лёгкое недовольство. Возможно, им не хватает внимания или помощи"},
        {Health.Hopeless, "Жители деревни открыто выказывают своё раздражение. Им не нравится происходящее, и они ждут от вас действий"},
        {Health.Rage, "Жители деревни в ярости! Их терпение иссякло, и они готовы к решительным мерам. Ваше положение в деревне под угрозой"},
    };

    public void ShowMoodInfo(Health mood)
    {
        _textField.text = _moodDescriptions[mood];
        _infoWindow.Enable();
    }
}
