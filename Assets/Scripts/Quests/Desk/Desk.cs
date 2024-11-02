using System;
using System.Collections.Generic;

public class Desk
{
    private readonly List<Quest> _quests = new List<Quest>();
    private readonly IEventsLifeTimeInfoGetter _eventsInfoGetter;
    private readonly NewQuestInitializer _newQuestInitializer;
    private readonly TableInitializer _table;
    private readonly Days _currentDay;
    private readonly SaveLoadSystem _saveLoadSystem;

    public Desk(SaveLoadSystem saveLoadSystem, NewQuestInitializer newQuestInitializer, TableInitializer table,
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
        _table = table != null ?
            table : 
            throw new ArgumentNullException(nameof(table));
        _currentDay = current;
        CreateQuestList();
        _newQuestInitializer.QuestPlaced += OnQuestPlaced;
        _table.QuestPlaced += OnQuestPlaced;
    }

    public event Action<Quest> QuestPlaced;

    public IEnumerable<Quest> Quests => _quests;

    public void RemoveQuest(Quest quest)
    {
        if (_quests.Contains(quest) == false)
            throw new InvalidOperationException("Quest don't existing in Desk");

        _quests.Remove(quest);
    }

    public void SaveData()
    {
        List<SerializableQuest> quests = new List<SerializableQuest>();

        foreach (var quest in _quests)
            quests.Add(quest.Serialize());

        _saveLoadSystem.SavePlacedQuests(quests);
    }

    private void OnQuestPlaced(Quest quest)
    {
        if (quest == null)
            throw new ArgumentNullException(nameof(quest));

        quest.CalcExpireDate(_currentDay, _eventsInfoGetter.GetLifeTime(quest.EventName));
        _quests.Add(quest);
        QuestPlaced?.Invoke(quest);
    }

    private void CreateQuestList()
    {
        List<SerializableQuest> list = _saveLoadSystem.GetPlacedQuests();

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
