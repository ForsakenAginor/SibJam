using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Quests.QuestCreation.Controller
{
    public class NewQuestController : MonoBehaviour, IKey
    {
        [SerializeField] private Button _toBoardButton;
        [SerializeField] private Button _toTableButton;

        private AudioSource _toDeskSound;
        private AudioSource _toBagSound;

        private Action<IKey> Accept;
        private Action<IKey> Decline;

        private void OnDestroy()
        {
            _toBoardButton.onClick.RemoveListener(OnToBoardButtonClick);
            _toTableButton.onClick.RemoveListener(OnToTableButtonClick);
        }

        public void Init(AudioSource toDeskSound, AudioSource toBagSound, Action<IKey> accept, Action<IKey> decline)
        {
            _toDeskSound = toDeskSound != null ? toDeskSound : throw new ArgumentNullException(nameof(toDeskSound));
            _toBagSound = toBagSound != null ? toBagSound : throw new ArgumentNullException(nameof(toBagSound));
            Accept = accept;
            Decline = decline;
            _toBoardButton.onClick.AddListener(OnToBoardButtonClick);
            _toTableButton.onClick.AddListener(OnToTableButtonClick);
        }

        private void OnToTableButtonClick()
        {
            _toBagSound.Play();
            Decline?.Invoke(this);
            gameObject.SetActive(false);
        }

        private void OnToBoardButtonClick()
        {
            _toDeskSound.Play();
            Accept?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}