using System;

public interface IQuestsStorageInitializer
{
    public event Action<Quest> QuestTransfered;
}
