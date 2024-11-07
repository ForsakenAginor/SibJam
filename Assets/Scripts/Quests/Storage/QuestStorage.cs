using Assets.Scripts.EventConfiguration;
using Assets.Scripts.Quests.Storage.View;
using Assets.Scripts.SaveSystem;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Quests.Storage
{
    public class QuestStorage
    {
        private readonly List<Quest> _quests = new List<Quest>();
        private readonly IEventsLifeTimeInfoGetter _eventsInfoGetter;
        private readonly IQuestSorter _questGiver;
        private readonly IQuestsStorageInitializer _anotherStorage;
        private readonly Days _currentDay;

        public QuestStorage(IQuestSorter questGiver,
            IQuestsStorageInitializer table,
            Days current, IEventsLifeTimeInfoGetter configuration,
            List<SerializableQuest> loadData, Action<List<SerializableQuest>> saveDataCallback)
        {
            _eventsInfoGetter = configuration != null ?
                configuration :
                throw new ArgumentNullException(nameof(configuration));

            _questGiver = questGiver != null ?
                questGiver :
                throw new ArgumentNullException(nameof(questGiver));
            _anotherStorage = table != null ?
                table :
                throw new ArgumentNullException(nameof(table));
            Save = saveDataCallback != null ?
                saveDataCallback :
                throw new ArgumentNullException(nameof(saveDataCallback));

            _currentDay = current;
            CreateQuestList(loadData);

            _questGiver.QuestStored += OnQuestStored;
            _anotherStorage.QuestTransfered += OnQuestStored;
        }

        ~QuestStorage()
        {
            _questGiver.QuestStored -= OnQuestStored;
            _anotherStorage.QuestTransfered -= OnQuestStored;
        }

        public event Action<Quest> NewQuestTaken;
        public event Action QuestRemoved;

        private readonly Action<List<SerializableQuest>> Save;

        public IEnumerable<Quest> Quests => _quests;

        public void RemoveQuest(Quest quest)
        {
            if (_quests.Contains(quest) == false)
                throw new InvalidOperationException("Quest don't existing in Storage");

            _quests.Remove(quest);
            QuestRemoved?.Invoke();
        }

        public void SaveData()
        {
            List<SerializableQuest> quests = new List<SerializableQuest>();

            foreach (var quest in _quests)
                quests.Add(quest.Serialize());

            Save?.Invoke(quests);
        }

        private void OnQuestStored(Quest quest)
        {
            if (quest == null)
                throw new ArgumentNullException(nameof(quest));

            quest.CalcExpireDate(_currentDay, _eventsInfoGetter.GetLifeTime(quest.EventName));
            _quests.Add(quest);
            NewQuestTaken?.Invoke(quest);
        }

        private void CreateQuestList(IEnumerable<SerializableQuest> list)
        {
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
}