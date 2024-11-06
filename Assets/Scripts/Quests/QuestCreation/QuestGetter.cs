using Assets.Scripts.EventConfiguration;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Quests.QuestCreation
{
    public class QuestGetter
    {
        private readonly List<EventNames> _availableQuests;
        private readonly Days _currentDay;
        private readonly List<EventNames> _tutorialQuests = new();

        public QuestGetter(List<EventNames> availableQuests, Days currentDay)
        {
            _availableQuests = availableQuests != null ? availableQuests : throw new ArgumentNullException(nameof(availableQuests));
            _currentDay = currentDay;
            _tutorialQuests.Add(EventNames.Quest_ob_1);
            _tutorialQuests.Add(EventNames.Quest_ob_2);
            _tutorialQuests.Add(EventNames.Quest_ob_3);
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

        public Quest GetTutorialQuest()
        {
            if (_availableQuests.Count == 0 || _tutorialQuests.Count == 0)
                return null;

            int index = UnityEngine.Random.Range(0, _tutorialQuests.Count);
            EventNames name = _tutorialQuests[index];
            Quest quest = new Quest(name, _currentDay);
            _tutorialQuests.RemoveAt(index);
            _availableQuests.Remove(name);
            return quest;
        }
    }
}