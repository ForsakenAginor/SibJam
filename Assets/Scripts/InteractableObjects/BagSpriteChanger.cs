using Assets.Scripts.Quests;
using Assets.Scripts.Quests.Storage;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.InteractableObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BagSpriteChanger : MonoBehaviour
    {
        [SerializeField] private InteractableSprite _interactableSprite;
        [SerializeField] private Button _button;

        [Header("Sprites variation")]
        [SerializeField] private Sprite _openEmptySprite;
        [SerializeField] private Sprite _openFullSprite;
        [SerializeField] private Sprite _closeSprite;

        private SpriteRenderer _renderer;
        private QuestStorage _bag;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = _closeSprite;
            _interactableSprite.Pressed += OnPressed;
            _button.onClick.AddListener(OnButtonPressed);
        }

        private void OnDestroy()
        {
            _interactableSprite.Pressed -= OnPressed;
            _button.onClick.RemoveListener(OnButtonPressed);
        }

        public void Init(QuestStorage bag)
        {
            _bag = bag != null ? bag : throw new ArgumentNullException(nameof(bag));
            _bag.NewQuestTaken += OnPressed;
            _bag.QuestRemoved += OnPressed;
        }

        private void OnButtonPressed()
        {
            _renderer.sprite = _closeSprite;
        }

        private void OnPressed(Quest _)
        {
            _interactableSprite.PlayPulseAnimation();
            OnPressed();
        }

        private void OnPressed()
        {
            if (_bag.Quests.Count() > 0 && _interactableSprite.IsEnabled == false)
                _renderer.sprite = _openFullSprite;
            else if (_bag.Quests.Count() == 0 && _interactableSprite.IsEnabled == false)
                _renderer.sprite = _openEmptySprite;
        }
    }
}