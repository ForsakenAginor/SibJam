using System;

namespace Assets.Scripts.Quests.Storage.View
{
    public interface IQuestsStorageInitializer
    {
        public event Action<Quest> QuestTransfered;
    }
}