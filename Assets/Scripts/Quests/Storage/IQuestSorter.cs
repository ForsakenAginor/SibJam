using System;

namespace Assets.Scripts.Quests.Storage
{
    public interface IQuestSorter
    {
        public event Action<Quest> QuestStored;
    }
}