using System;
using System.Collections.Generic;
using UnityEngine;

public class DeskInitializer : MonoBehaviour
{
    private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

    [SerializeField] private StoredQuestView _storedQuestViewPrefab;
    [SerializeField] private QuestIcon _questIconPrefab;
    [SerializeField] private Transform _questViewHolder;
    [SerializeField] private Transform _iconHolder;
    [SerializeField] private Sprite _image;

    private IEventsInfoGetter _eventsInfoGetter;
    private Desk _desk;

    public event Action<Quest> QuestStored;

    private void OnDestroy()
    {
        _desk.QuestPlaced -= OnQuestPlaced;
    }

    public void Init(Desk desk, IEventsInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
        _desk = desk != null ? desk : throw new ArgumentNullException(nameof(desk));

        foreach (var quest in _desk.Quests)
            CreateView(quest);

        _desk.QuestPlaced += OnQuestPlaced;
    }

    private void OnQuestPlaced(Quest quest)
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
        _desk.RemoveQuest(_quests[key]);
        QuestStored?.Invoke(_quests[key]);
    }
}