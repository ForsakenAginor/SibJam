using System;
using System.Collections.Generic;

public class RandomQuestGetter
{
    private readonly List<EventNames> _availableQuests;
    private readonly Days _currentDay;

    public RandomQuestGetter(List<EventNames> availableQuests, Days currentDay)
    {
        _availableQuests = availableQuests != null ? availableQuests : throw new ArgumentNullException(nameof(availableQuests));
        _currentDay = currentDay;
    }

    public Quest GetRandomQuest()
    {
        if (_availableQuests.Count == 0)
            return null;

        int index = UnityEngine.Random.Range(0, _availableQuests.Count);
        Quest quest = new Quest(_availableQuests[index], _currentDay);
        _availableQuests.RemoveAt(index);
        return quest;
    }
}
