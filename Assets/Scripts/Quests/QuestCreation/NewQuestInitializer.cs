using System;
using System.Collections.Generic;
using UnityEngine;

public class NewQuestInitializer : MonoBehaviour
{
    private readonly Dictionary<IKey, Quest> _quests = new Dictionary<IKey, Quest>();

    [SerializeField] private NewQuestView _newQuestViewPrefab;
    [SerializeField] private Transform _holder;
    [SerializeField] private Sprite _image;
    [SerializeField] private AudioSource _toDeskSound;
    [SerializeField] private AudioSource _toBagSound;

    private IEventsInfoGetter _eventsInfoGetter;

    public event Action<Quest> QuestPlaced;
    public event Action<Quest> QuestStored;

    public void Init(IEnumerable<Quest> newQuests, IEventsInfoGetter configuration, List<InteractablePeasant> peasants)
    {
        if(peasants == null)
            throw new ArgumentNullException(nameof(peasants));

        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));

        if (newQuests == null)
            throw new ArgumentNullException(nameof(newQuests));

        List<UIElement> list = new List<UIElement>();
        int iterator = 0;
        float step = 40;

        foreach(var newQuest in newQuests)
        {
            var view = Instantiate(_newQuestViewPrefab, _holder);
            IKey key = view.GetKey();
            _quests.Add(key, newQuest);
            view.Init(_image,
                _eventsInfoGetter.GetName(newQuest.EventName),
                _eventsInfoGetter.GetDescription(newQuest.EventName),
                _toDeskSound,
                _toBagSound,
                QuestPlacedCallback,
                QuestStoredCallback);
            RectTransform rect = view.GetComponent<RectTransform>();
            rect.offsetMin += new Vector2(step * iterator, -step * iterator);
            rect.offsetMax += new Vector2(step * iterator, -step * iterator);
            UIElement ui = view.GetUIElement();
            ui.Disable();
            list.Add(ui);
            iterator++;
        }

        if (peasants.Count != list.Count)
            throw new Exception("peasants count don't equal quests count");

        for(int i = 0; i < peasants.Count; i++)
            peasants[i].Init(list[i]);        
    }

    private void QuestPlacedCallback(IKey key)
    {
        QuestPlaced?.Invoke(_quests[key]);
    }

    private void QuestStoredCallback(IKey key)
    {
        QuestStored?.Invoke(_quests[key]);
    }
}

public interface IQuestSorter
{
    public event Action<Quest> QuestStored;
}

public class BagAdapter : IQuestSorter
{
    private readonly NewQuestInitializer _questGiver;

    public BagAdapter(NewQuestInitializer questGiver)
    {
        _questGiver = questGiver != null ? questGiver : throw new ArgumentNullException(nameof(questGiver));
        _questGiver.QuestStored += OnQuestTaken;
    }

    ~ BagAdapter()
    {
        _questGiver.QuestStored -= OnQuestTaken;
    }

    public event Action<Quest> QuestStored;

    private void OnQuestTaken(Quest quest)
    {
        QuestStored?.Invoke(quest);
    }
}

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
