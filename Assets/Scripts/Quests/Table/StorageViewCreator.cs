using System;
using System.Collections.Generic;
using UnityEngine;

public class StorageViewCreator : MonoBehaviour, IQuestsStorageInitializer
{
    private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

    [SerializeField] private StoredQuestView _storedQuestViewPrefab;
    [SerializeField] private QuestIcon _questIconPrefab;
    [SerializeField] private Transform _questViewHolder;
    [SerializeField] private Transform _iconHolder;
    [SerializeField] private AudioSource _buttonClickSource;

    private IEventsInfoGetter _eventsInfoGetter;
    private QuestStorage _bag;

    public event Action<Quest> QuestTransfered;

    private void OnDestroy()
    {
        _bag.NewQuestTaken -= OnQuestStored;
    }

    public void Init(QuestStorage bag, IEventsInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
        _bag = bag != null ? bag : throw new ArgumentNullException(nameof(bag));

        foreach (var quest in _bag.Quests)        
            CreateView(quest);

        _bag.NewQuestTaken += OnQuestStored;
    }

    private void OnQuestStored(Quest quest)
    {
        CreateView(quest);
    }

    private void CreateView(Quest quest)
    {
        var view = Instantiate(_storedQuestViewPrefab, _questViewHolder);
        var icon = Instantiate(_questIconPrefab, _iconHolder);
        _quests.Add(icon, quest);
        view.Init(_eventsInfoGetter.GetName(quest.EventName),
            _eventsInfoGetter.GetDescription(quest.EventName),
            quest.DaysToExpire);
        view.GetUIElement().Disable();
        icon.Init(view.GetUIElement(), _buttonClickSource, QuestPlacedCallback);
    }

    private void QuestPlacedCallback(IKey key)
    {
        _bag.RemoveQuest(_quests[key]);
        QuestTransfered?.Invoke(_quests[key]);
    }
}