using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DayView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private float _animationDuration = 3f;

    private Dictionary<Days, string> _daysColors = new Dictionary<Days, string>()
    {
            {Days.Monday, "1 День"},
            {Days.Tuesday, "2 День"},
            {Days.Wednesday, "3 День"},
            {Days.Thursday, "4 День"},
            {Days.Friday, "5 День"},
            {Days.Saturday, "6 День"},
            {Days.Sunday, "7 День"},
            {Days.Final, "ФИНАЛЬНЫЙ ДЕНЬ"}
    };

    public void Init(Days currentDay)
    {
        _textField.text = _daysColors[currentDay];
        _textField.DOFade(1f, _animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutCubic);
    }
}
