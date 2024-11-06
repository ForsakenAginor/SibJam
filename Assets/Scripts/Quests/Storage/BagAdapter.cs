using Assets.Scripts.Quests.QuestCreation.View;
using System;

namespace Assets.Scripts.Quests.Storage
{
    public class BagAdapter : IQuestSorter
    {
        private readonly NewQuestInitializer _questGiver;

        public BagAdapter(NewQuestInitializer questGiver)
        {
            _questGiver = questGiver != null ? questGiver : throw new ArgumentNullException(nameof(questGiver));
            _questGiver.QuestStored += OnQuestTaken;
        }

        ~BagAdapter()
        {
            _questGiver.QuestStored -= OnQuestTaken;
        }

        public event Action<Quest> QuestStored;

        private void OnQuestTaken(Quest quest)
        {
            QuestStored?.Invoke(quest);
        }
    }
}