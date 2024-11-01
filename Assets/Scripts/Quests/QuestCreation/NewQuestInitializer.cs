using System;
using System.Collections.Generic;
using UnityEngine;

public class NewQuestInitializer : MonoBehaviour
{
    [SerializeField] private NewQuestView _newQuestViewPrefab;
    [SerializeField] private Transform _holder;
    [SerializeField] private Sprite _image;

    private IEventsInfoGetter _eventsInfoGetter;

    public void Init(IEnumerable<Quest> newQuests, IEventsInfoGetter configuration)
    {
        _eventsInfoGetter = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));

        if (newQuests == null)
            throw new ArgumentNullException(nameof(newQuests));

        foreach(var newQuest in newQuests)
        {
            var view = Instantiate(_newQuestViewPrefab, _holder);
            view.Init(_image,
                _eventsInfoGetter.GetName(newQuest.EventName),
                _eventsInfoGetter.GetDescription(newQuest.EventName));
        }
    }
}
