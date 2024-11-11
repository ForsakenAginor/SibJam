using Assets.Scripts.EventConfiguration;
using Assets.Scripts.General;
using Assets.Scripts.InteractableObjects;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Quests.QuestCreation.View
{
    public class NewQuestInitializer : MonoBehaviour
    {
        private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

        [SerializeField] private NewQuestView _newQuestViewPrefab;
        [SerializeField] private Transform _holder;

        [Header("Sounds")]
        [SerializeField] private AudioSource _toDeskSound;
        [SerializeField] private AudioSource _toBagSound;

        [Header("Cascade effect delta settings")]
        [SerializeField] private float _xStep = 40;
        [SerializeField] private float _yStep = -40;

        private IEventsInfoGetter _eventsInfoGetter;

        public event Action<Quest> QuestPlaced;
        public event Action<Quest> QuestStored;

        public void Init(IEnumerable<Quest> newQuests, IEventsInfoGetter configuration, List<InteractableSprite> peasants)
        {
            if (peasants == null)
                throw new ArgumentNullException(nameof(peasants));

            _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));

            if (newQuests == null)
                throw new ArgumentNullException(nameof(newQuests));

            List<UIElement> list = new List<UIElement>();
            int iterator = 0;

            foreach (var newQuest in newQuests)
            {
                var view = Instantiate(_newQuestViewPrefab, _holder);
                IKey key = view.GetKey();
                _quests.Add(key, newQuest);
                view.Init(_eventsInfoGetter.GetName(newQuest.EventName),
                    _eventsInfoGetter.GetDescription(newQuest.EventName),
                    _toDeskSound,
                    _toBagSound,
                    QuestPlacedCallback,
                    QuestStoredCallback);

                RectTransform rect = view.GetComponent<RectTransform>();
                rect.offsetMin += new Vector2(_xStep * iterator, _yStep * iterator);
                rect.offsetMax += new Vector2(_xStep * iterator, _yStep * iterator);
                iterator++;

                UIElement ui = view.GetUIElement();
                ui.Disable();
                list.Add(ui);
            }
            
            if (peasants.Count != list.Count)
                throw new Exception("peasants count don't equal quests count");
            
            for (int i = 0; i < list.Count; i++)
                peasants[i].Init(list[i]);
        }

        private void QuestPlacedCallback(IKey key)
        {
            QuestPlaced?.Invoke(_quests[key]);
        }

        private void QuestStoredCallback(IKey key)
        {
            QuestStored?.Invoke(_quests[key]);
        }
    }
}