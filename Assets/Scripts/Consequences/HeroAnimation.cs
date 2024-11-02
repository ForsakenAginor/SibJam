using DG.Tweening;
using System.Collections;
using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _heroSprite;
    [SerializeField] private float _duration;

    private void Start()
    {
        StartCoroutine(PlayAnimation());    
    }

    private IEnumerator PlayAnimation()
    {
        WaitForSeconds delay = new WaitForSeconds(_duration);
        _heroSprite.DOMove(_endPoint.position, _duration).SetEase(Ease.Linear);
        yield return delay;
    }
}
