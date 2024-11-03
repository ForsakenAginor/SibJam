using System;
using System.Collections.Generic;

public class Table
{
    private readonly List<Quest> _quests = new List<Quest>();
    private readonly IEventsLifeTimeInfoGetter _eventsInfoGetter;
    private readonly NewQuestInitializer _newQuestInitializer;
    private readonly DeskInitializer _desk;
    private readonly Days _currentDay;
    private readonly SaveLoadSystem _saveLoadSystem;

    public Table(SaveLoadSystem saveLoadSystem, NewQuestInitializer newQuestInitializer,
        DeskInitializer desk,
        Days current, IEventsLifeTimeInfoGetter configuration)
    {
        _saveLoadSystem = saveLoadSystem != null ?
            saveLoadSystem :
            throw new ArgumentNullException(nameof(saveLoadSystem));
        _eventsInfoGetter = configuration != null ?
            configuration :
            throw new ArgumentNullException(nameof(configuration));
        _newQuestInitializer = newQuestInitializer != null ?
            newQuestInitializer :
            throw new ArgumentNullException(nameof(newQuestInitializer));
        _desk = desk != null ?
            desk :
            throw new ArgumentNullException(nameof(desk));
        _currentDay = current;
        CreateQuestList();
        _newQuestInitializer.QuestStored += OnQuestStored;
        _desk.QuestStored += OnQuestStored;
    }

    ~Table()
    {
        _newQuestInitializer.QuestStored -= OnQuestStored;
        _desk.QuestStored -= OnQuestStored;
    }

    public event Action<Quest> QuestStored;
    public event Action QuestRemoved;

    public IEnumerable<Quest> Quests => _quests;

    public void RemoveQuest(Quest quest)
    {
        if (_quests.Contains(quest) == false)
            throw new InvalidOperationException("Quest don't existing in table");

        _quests.Remove(quest);
        QuestRemoved?.Invoke();
    }

    public void SaveData()
    {
        List<SerializableQuest> quests = new List<SerializableQuest>();

        foreach(var quest in _quests)
            quests.Add(quest.Serialize());

        _saveLoadSystem.SaveStoredQuests(quests);
    }

    private void OnQuestStored(Quest quest)
    {
        if (quest == null)
            throw new ArgumentNullException(nameof(quest));

        quest.CalcExpireDate(_currentDay, _eventsInfoGetter.GetLifeTime(quest.EventName));
        _quests.Add(quest);
        QuestStored?.Invoke(quest);
    }

    private void CreateQuestList()
    {
        List<SerializableQuest> list = _saveLoadSystem.GetStoredQuests();

        if (list == null)
            return;

        foreach (var item in list)
        {
            Quest quest = new(item.EventName, item.DayObtain);
            quest.CalcExpireDate(_currentDay, _eventsInfoGetter.GetLifeTime(quest.EventName));
            _quests.Add(quest);        
        }
    }
}
