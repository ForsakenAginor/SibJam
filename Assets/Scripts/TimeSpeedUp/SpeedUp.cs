using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TimeSpeedUp
{
    public class SpeedUp : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _timeBoostValue = 3f;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            Time.timeScale = 1f;
        }

        private void OnButtonClick()
        {
            _button.interactable = false;
            Time.timeScale = _timeBoostValue;
        }
    }
}