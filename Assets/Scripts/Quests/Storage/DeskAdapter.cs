using Assets.Scripts.Quests.QuestCreation.View;
using System;

namespace Assets.Scripts.Quests.Storage
{
    public class DeskAdapter : IQuestSorter
    {
        private readonly NewQuestInitializer _questGiver;

        public DeskAdapter(NewQuestInitializer questGiver)
        {
            _questGiver = questGiver != null ? questGiver : throw new ArgumentNullException(nameof(questGiver));
            _questGiver.QuestPlaced += OnQuestTaken;
        }

        ~DeskAdapter()
        {
            _questGiver.QuestPlaced -= OnQuestTaken;
        }

        public event Action<Quest> QuestStored;

        private void OnQuestTaken(Quest quest)
        {
            QuestStored?.Invoke(quest);
        }
    }
}