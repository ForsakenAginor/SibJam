using System.Collections.Generic;
using DG.Tweening;
using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.DaySystem
{
    public class DayView : MonoBehaviour
    {
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedText;
        [SerializeField] private float _animationDuration = 3f;

        public void Init(Days currentDay)
        {
            _localizedText.TranslationName = currentDay.ToString();
            _localizedText.UpdateLocalization();
            TMP_Text text = _localizedText.GetComponent<TMP_Text>();
            text.DOFade(1f, _animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutCubic);
        }
    }
}