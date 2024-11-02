using System.Collections.Generic;
using System;
using UnityEngine;

public class ConsequencesCardsShower : MonoBehaviour
{
    private readonly List<IKey> _quests = new List<IKey>();

    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private Transform _holder;
    [SerializeField] private AudioSource _audioSource;

    private IEventsInfoGetter _eventsInfoGetter;

    public event Action AllEventsShown;
    public event Action<Quest> QuestExpired;

    public void Init(IEnumerable<Quest> expiredQuests, Quest chosedQuest, IEventsInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));

        if (expiredQuests == null)
            throw new ArgumentNullException(nameof(expiredQuests));


        foreach (var quest in expiredQuests)
        {
            var view = Instantiate(_cardPrefab, _holder);
            view.Init(_eventsInfoGetter.GetFailureSprite(quest.EventName),
                _eventsInfoGetter.GetName(quest.EventName),
                _eventsInfoGetter.GetFailDescription(quest.EventName),
                _audioSource,
                OnQuestShown);
            _quests.Add(view.GetKey());
            QuestExpired?.Invoke(quest);
        }

        if(chosedQuest != null)
        {
            var view = Instantiate(_cardPrefab, _holder);
            view.Init(_eventsInfoGetter.GetSuccessSprite(chosedQuest.EventName),
                _eventsInfoGetter.GetName(chosedQuest.EventName),
                _eventsInfoGetter.GetSuccessDescription(chosedQuest.EventName),
                _audioSource,
                OnQuestShown);
            _quests.Add(view.GetKey());
        }

        if (_quests.Count == 0)
            AllEventsShown?.Invoke();
    }

    private void OnQuestShown(IKey key)
    {
        if (_quests.Contains(key))
            _quests.Remove(key);

        if(_quests.Count == 0)
            AllEventsShown?.Invoke();
    }
}
