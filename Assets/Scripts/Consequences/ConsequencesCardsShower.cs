﻿using System.Collections.Generic;
using System;
using UnityEngine;

public class ConsequencesCardsShower : MonoBehaviour
{
    private readonly List<IKey> _quests = new List<IKey>();

    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private Transform _holder;

    private IEventsInfoGetter _eventsInfoGetter;

    public event Action AllEventsShown;

    public void Init(IEnumerable<Quest> expiredQuests, IEventsInfoGetter configuration)
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
