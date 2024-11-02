using System;
using System.Collections.Generic;
using UnityEngine;

public class NewQuestInitializer : MonoBehaviour
{
    private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

    [SerializeField] private NewQuestView _newQuestViewPrefab;
    [SerializeField] private Transform _holder;
    [SerializeField] private Sprite _image;

    private IEventsInfoGetter _eventsInfoGetter;

    public event Action<Quest> QuestPlaced;
    public event Action<Quest> QuestStored;

    public void Init(IEnumerable<Quest> newQuests, IEventsInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));

        if (newQuests == null)
            throw new ArgumentNullException(nameof(newQuests));

        foreach(var newQuest in newQuests)
        {
            var view = Instantiate(_newQuestViewPrefab, _holder);
            IKey key = view.GetKey();
            _quests.Add(key, newQuest);
            view.Init(_image,
                _eventsInfoGetter.GetName(newQuest.EventName),
                _eventsInfoGetter.GetDescription(newQuest.EventName),
                QuestPlacedCallback,
                QuestStoredCallback);
        }
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
