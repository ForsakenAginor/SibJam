using System;

public class QuestAcceptingMonitor
{
    private readonly NewQuestInitializer _questGiver;
    private int _count = 3;

    public QuestAcceptingMonitor(NewQuestInitializer questGiver)
    {
        _questGiver = questGiver != null ? questGiver : throw new ArgumentNullException(nameof(questGiver));
        _questGiver.QuestPlaced += OnQuestHandled;
        _questGiver.QuestStored += OnQuestHandled;
    }

    ~QuestAcceptingMonitor()
    {
        _questGiver.QuestPlaced -= OnQuestHandled;
        _questGiver.QuestStored -= OnQuestHandled;
    }

    public event Action AllQuestsHandled;

    private void OnQuestHandled(Quest _)
    {
        _count--;

        if (_count == 0)
            AllQuestsHandled?.Invoke();
    }
}
