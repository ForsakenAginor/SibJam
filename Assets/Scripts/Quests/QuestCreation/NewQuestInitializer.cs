using System;
using System.Collections.Generic;
using UnityEngine;

public class NewQuestInitializer : MonoBehaviour
{
    private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

    [SerializeField] private NewQuestView _newQuestViewPrefab;
    [SerializeField] private Transform _holder;
    [SerializeField] private Sprite _image;
    [SerializeField] private AudioSource _audioSource;

    private IEventsInfoGetter _eventsInfoGetter;

    public event Action<Quest> QuestPlaced;
    public event Action<Quest> QuestStored;

    public void Init(IEnumerable<Quest> newQuests, IEventsInfoGetter configuration, List<InteractablePeasant> peasants)
    {
        if(peasants == null)
            throw new ArgumentNullException(nameof(peasants));

        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));

        if (newQuests == null)
            throw new ArgumentNullException(nameof(newQuests));

        List<UIElement> list = new List<UIElement>();

        foreach(var newQuest in newQuests)
        {
            var view = Instantiate(_newQuestViewPrefab, _holder);
            IKey key = view.GetKey();
            _quests.Add(key, newQuest);
            view.Init(_image,
                _eventsInfoGetter.GetName(newQuest.EventName),
                _eventsInfoGetter.GetDescription(newQuest.EventName),
                _audioSource,
                QuestPlacedCallback,
                QuestStoredCallback);
            UIElement ui = view.GetUIElement();
            ui.Disable();
            list.Add(ui);
        }

        if (peasants.Count != list.Count)
            throw new Exception("peasants count don't equal quests count");

        for(int i = 0; i < peasants.Count; i++)
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
