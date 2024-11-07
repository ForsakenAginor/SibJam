using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Consequences
{
    public class HeroAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Transform _heroSprite;
        [SerializeField] private float _stepFrequence;
        [SerializeField] private float _stepValue;
        [SerializeField] private AudioSource _stepsSound;
        private float _duration;

        public void Init(float duration)
        {
            _duration = duration > 0 ? duration : throw new ArgumentOutOfRangeException(nameof(duration));
            StartCoroutine(PlayAnimation());
            StartCoroutine(FlipSprite());
        }

        private IEnumerator PlayAnimation()
        {
            WaitForSeconds delay = new WaitForSeconds(_duration);
            _heroSprite.DOMove(_endPoint.position, _duration).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
            float stepValue = _heroSprite.position.y + _stepValue;
            _heroSprite.DOMoveY(stepValue, _stepFrequence).SetLoops(-1, LoopType.Yoyo);
            yield return delay;
        }

        private IEnumerator FlipSprite()
        {
            WaitForSeconds delay = new WaitForSeconds(_duration);
            yield return delay;
            _heroSprite.GetComponent<SpriteRenderer>().flipX = true;
            yield return delay;
            _stepsSound.Pause();
        }
    }
}