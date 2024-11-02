using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInitializer : MonoBehaviour
{
    private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

    [SerializeField] private StoredQuestView _storedQuestViewPrefab;
    [SerializeField] private QuestIcon _questIconPrefab;
    [SerializeField] private Transform _questViewHolder;
    [SerializeField] private Transform _iconHolder;
    [SerializeField] private Sprite _image;

    private IEventsInfoGetter _eventsInfoGetter;
    private Table _table;

    public event Action<Quest> QuestPlaced;

    private void OnDestroy()
    {
        _table.QuestStored -= OnQuestStored;
    }

    public void Init(Table table, IEventsInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
        _table = table != null ? table : throw new ArgumentNullException(nameof(table));

        foreach (var quest in _table.Quests)        
            CreateView(quest);

        _table.QuestStored += OnQuestStored;
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
        view.Init(_image,
            _eventsInfoGetter.GetName(quest.EventName),
            _eventsInfoGetter.GetDescription(quest.EventName),
            quest.DaysToExpire);
        view.GetUIElement().Disable();
        icon.Init(view.GetUIElement(), QuestPlacedCallback);
    }

    private void QuestPlacedCallback(IKey key)
    {
        _table.RemoveQuest(_quests[key]);
        QuestPlaced?.Invoke(_quests[key]);
    }
}
