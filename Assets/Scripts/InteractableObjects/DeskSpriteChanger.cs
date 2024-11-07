using Assets.Scripts.Quests;
using Assets.Scripts.Quests.Storage;
using Assets.Scripts.UI;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.InteractableObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DeskSpriteChanger : MonoBehaviour
    {
        [SerializeField] private UIElement _mark;
        [SerializeField] private InteractableSprite _interactableSprite;

        [Header("Sprites variation")]
        [SerializeField] private Sprite _emptyDesk;
        [SerializeField] private Sprite _fullDesk;
        [SerializeField] private Sprite _oneQuestDesk;
        [SerializeField] private Sprite _threeQuestDesk;

        private SpriteRenderer _renderer;
        private QuestStorage _desk;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _interactableSprite.Pressed += OnContainChanged;
        }

        private void OnDestroy()
        {
            _interactableSprite.Pressed -= OnContainChanged;
        }

        public void Init(QuestStorage table)
        {
            _desk = table != null ? table : throw new ArgumentNullException(nameof(table));
            OnContainChanged();
            _desk.NewQuestTaken += OnContainChanged;
            _desk.QuestRemoved += OnContainChanged;
        }

        private void OnContainChanged(Quest _)
        {
            _interactableSprite.PlayPulseAnimation();
            OnContainChanged();
        }

        private void OnContainChanged()
        {
            int quests = _desk.Quests.Count();

            if (quests == 0)
            {
                _renderer.sprite = _emptyDesk;
                _mark.Disable();
                return;
            }

            if (quests >= 1 && quests < 3)
                _renderer.sprite = _oneQuestDesk;
            else if (quests == 3)
                _renderer.sprite = _threeQuestDesk;
            else
                _renderer.sprite = _fullDesk;

            _mark.Enable();
        }
    }
}