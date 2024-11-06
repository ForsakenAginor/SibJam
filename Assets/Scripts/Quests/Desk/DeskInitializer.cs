using System;
using System.Collections.Generic;
using UnityEngine;

public class DeskInitializer : MonoBehaviour, IQuestsStorageInitializer
{
    private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

    [SerializeField] private StoredQuestView _storedQuestViewPrefab;
    [SerializeField] private QuestIcon _questIconPrefab;
    [SerializeField] private Transform _questViewHolder;
    [SerializeField] private Transform _iconHolder;
    [SerializeField] private Sprite _image;
    [SerializeField] private AudioSource _buttonClickSource;

    private IEventsInfoGetter _eventsInfoGetter;
    private QuestStorage _desk;

    public event Action<Quest> QuestTransfered;

    private void OnDestroy()
    {
        _desk.NewQuestTaken -= OnQuestPlaced;
    }

    public void Init(QuestStorage desk, IEventsInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
        _desk = desk != null ? desk : throw new ArgumentNullException(nameof(desk));

        foreach (var quest in _desk.Quests)
            CreateView(quest);

        _desk.NewQuestTaken += OnQuestPlaced;
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
        icon.Init(view.GetUIElement(), _buttonClickSource, QuestPlacedCallback);
    }

    private void QuestPlacedCallback(IKey key)
    {
        _desk.RemoveQuest(_quests[key]);
        QuestTransfered?.Invoke(_quests[key]);
    }
}