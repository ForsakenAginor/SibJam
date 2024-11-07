using Assets.Scripts.DaySystem;
using Assets.Scripts.EventConfiguration;
using Assets.Scripts.Mood;
using System.Collections.Generic;

namespace Assets.Scripts.SaveSystem
{
    public class SaveLoadSystem
    {
        private const string AvailableQuests = nameof(AvailableQuests);
        private const string StoredQuests = nameof(StoredQuests);
        private const string PlacedQuests = nameof(PlacedQuests);
        private const string Mood = nameof(Mood);

        private readonly DataStorage<List<EventNames>> _availableQuests;
        private readonly DataStorage<List<SerializableQuest>> _storedQuests;
        private readonly DataStorage<List<SerializableQuest>> _placedQuests;
        private readonly DataStorage<Health> _peoplesMood;
        private readonly DayData _dayData;

        public SaveLoadSystem()
        {
            _dayData = new();
            bool isMonday = false;

            if (_dayData.GetCurrentDay() == Days.Monday)
                isMonday = true;

            _availableQuests = new DataStorage<List<EventNames>>(AvailableQuests, isMonday);
            _storedQuests = new DataStorage<List<SerializableQuest>>(StoredQuests, isMonday);
            _placedQuests = new DataStorage<List<SerializableQuest>>(PlacedQuests, isMonday);
            _peoplesMood = new DataStorage<Health>(Mood, isMonday);
        }

        public List<EventNames> GetAvailableQuests() => _availableQuests.LoadData();

        public void SaveAvailableQuests(List<EventNames> quests) => _availableQuests.SaveData(quests);

        public List<SerializableQuest> GetStoredQuests() => _storedQuests.LoadData();

        public void SaveStoredQuests(List<SerializableQuest> quests) => _storedQuests.SaveData(quests);

        public List<SerializableQuest> GetPlacedQuests() => _placedQuests.LoadData();

        public void SavePlacedQuests(List<SerializableQuest> quests) => _placedQuests.SaveData(quests);

        public Health GetMood() => _peoplesMood.LoadData();

        public void SaveMood(Health mood) => _peoplesMood.SaveData(mood);

        public Days GetCurrentDay() => _dayData.GetCurrentDay();

        public void SaveDay() => _dayData.SaveDay();

        public void RemoveDayData() => _dayData.RemoveData();
    }
}