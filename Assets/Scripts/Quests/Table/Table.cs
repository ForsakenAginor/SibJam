using System;
using System.Collections.Generic;

public class Table
{
    private readonly List<Quest> _quests = new List<Quest>();
    private readonly IEventsLifeTimeInfoGetter _eventsInfoGetter;
    private readonly NewQuestInitializer _newQuestInitializer;
    private readonly Days _currentDay;

    public Table(NewQuestInitializer newQuestInitializer, Days current, IEventsLifeTimeInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ?
            configuration :
            throw new ArgumentNullException(nameof(configuration));
        _newQuestInitializer = newQuestInitializer != null ?
            newQuestInitializer :
            throw new ArgumentNullException(nameof(newQuestInitializer));
        _currentDay = current;

        _newQuestInitializer.QuestStored += OnQuestStored;
    }

    public event Action<Quest> QuestStored;

    public IEnumerable<Quest> Quests => _quests;

    public void RemoveQuest(Quest quest)
    {
        if (_quests.Contains(quest) == false)
            throw new InvalidOperationException("Quest don't existing in table");

        _quests.Remove(quest);
    }

    private void OnQuestStored(Quest quest)
    {
        if (quest == null)
            throw new ArgumentNullException(nameof(quest));

        quest.CalcExpireDate(_currentDay, _eventsInfoGetter.GetLifeTime(quest.EventName));
        _quests.Add(quest);
        QuestStored?.Invoke(quest);
    }
}
