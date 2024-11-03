using System;
using System.Collections.Generic;

public class NewQuestCreator
{
    private readonly SaveLoadSystem _saveLoadSystem;
    private readonly List<EventNames> _availableQuests;
    private readonly List<Quest> _newQuests = new List<Quest>();

    public NewQuestCreator(SaveLoadSystem saveLoadSystem, Days currentDay, int questsAmount = 3)
    {
        if (questsAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(questsAmount));

        _saveLoadSystem = saveLoadSystem != null
            ? saveLoadSystem
            : throw new ArgumentNullException(nameof(saveLoadSystem));

        _availableQuests = _saveLoadSystem.GetAvailableQuests();

        if (_availableQuests == null)
        {
            _availableQuests = new();

            foreach (var i in Enum.GetValues(typeof(EventNames)))
                _availableQuests.Add((EventNames)i);
        }


        RandomQuestGetter randomQuestGetter = new(_availableQuests, currentDay);

        if (currentDay != Days.Monday)
        {
            for (int i = 0; i < questsAmount; i++)
            {
                Quest quest = randomQuestGetter.GetRandomQuest();

                if (quest != null)
                    _newQuests.Add(quest);
            }
        }
        else
        {
            for (int i = 0; i < questsAmount; i++)
            {
                Quest quest = randomQuestGetter.GetTutorialQuest();

                if (quest != null)
                    _newQuests.Add(quest);
            }
        }


        _saveLoadSystem.SaveAvailableQuests(_availableQuests);
    }

    public List<Quest> NewQuests => _newQuests;
}
