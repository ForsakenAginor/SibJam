using Assets.Scripts.EventConfiguration;
using Assets.Scripts.Mood;
using Assets.Scripts.Quests;
using Assets.Scripts.SaveSystem;
using System;

namespace Assets.Scripts.Consequences
{
    public class HealthDamageSystem
    {
        private readonly ConsequencesCardsShower _questManager;
        private readonly IImportantInfoGetter _importantInfoGetter;
        private readonly SaveLoadSystem _saveLoadSystem;

        private Health _mood;

        public HealthDamageSystem(ConsequencesCardsShower questManager, IImportantInfoGetter importantInfoGetter, SaveLoadSystem saveLoadSystem)
        {
            _questManager = questManager != null ? questManager : throw new ArgumentNullException(nameof(questManager));
            _importantInfoGetter = importantInfoGetter != null ? importantInfoGetter : throw new ArgumentNullException(nameof(importantInfoGetter));
            _saveLoadSystem = saveLoadSystem != null ? saveLoadSystem : throw new ArgumentNullException(nameof(saveLoadSystem));

            _mood = _saveLoadSystem.GetMood();
            questManager.QuestExpired += OnQuestExpired;
        }

        ~HealthDamageSystem()
        {
            _questManager.QuestExpired -= OnQuestExpired;
        }

        public void SaveMood()
        {
            _saveLoadSystem.SaveMood(_mood);
        }

        private void OnQuestExpired(Quest quest)
        {
            bool isImportant = _importantInfoGetter.GetImportantStatus(quest.EventName);

            if (isImportant && _mood != Health.Riot)
                _mood = (Health)((int)_mood + 1);
        }
    }
}