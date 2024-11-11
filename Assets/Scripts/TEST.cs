using Assets.Scripts.Consequences;
using Assets.Scripts.EventConfiguration;
using Assets.Scripts.General;
using Assets.Scripts.InteractableObjects;
using Assets.Scripts.Quests;
using Assets.Scripts.Quests.QuestCreation.View;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NewQuestInitializer))]
[RequireComponent(typeof(ConsequencesCardsShower))]
public class TEST : MonoBehaviour
{
    [SerializeField] private EventsConfiguration _eventsConfiguration;
    [SerializeField] private Button _button;
    [SerializeField] private List<InteractableSprite> _interactables;

    private ConsequencesCardsShower _consequencesCardsShower;
    private NewQuestInitializer _questInitializer;
    private List<Quest> _quests = new List<Quest>();
    private List<Quest> _fullList = new List<Quest>();

    private void Awake()
    {
        _consequencesCardsShower = GetComponent<ConsequencesCardsShower>();
        _questInitializer = GetComponent<NewQuestInitializer>();
    }

    private void Start()
    {
        SceneChangerSingleton.Instance.FadeOut();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _button.interactable = false;

        foreach (var quest in Enum.GetValues(typeof(EventNames)))
        {
            Quest item = new Quest((EventNames)quest, Assets.Scripts.DaySystem.Days.Monday);
            List<Quest> list = new List<Quest>();
            list.Add(item);
            _quests.Add(item);
            _fullList.Add(item);
            _consequencesCardsShower.Init(_quests, list, _eventsConfiguration);
            _quests.Clear();
        }

        _questInitializer.Init(_fullList, _eventsConfiguration, _interactables);
    }
}
