using Sound;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [SerializeField] private EventsConfiguration _eventsConfiguration;
    [SerializeField] private SoundInitializer _soundInitializer;

    [SerializeField] private DayView _dayView;
    [SerializeField] private Button _nextDayButton;
    [SerializeField] private NewQuestInitializer _newQuestInitializer;
    [SerializeField] private TableInitializer _tableInitializer;
    [SerializeField] private DeskInitializer _deskInitializer;
    [SerializeField] private List<InteractablePeasant> _peasants;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private UIElement _loseScreen;

    private DayData _dayData;
    private Table _table;
    private Desk _desk;
    private QuestAcceptingMonitor _questAcceptingMonitor;
    private SaveLoadSystem _saveLoadSystem;
    private Health _mood;

    //*******************Delete***************
    [Header("TestOnly")]
    [SerializeField] private Button _resetButton;
    //****************************************

    private void Start()
    {
        _soundInitializer.Init();
        _nextDayButton.interactable = false;
        _dayData = new DayData();
        Days currentDay = _dayData.GetCurrentDay();
        _saveLoadSystem = new SaveLoadSystem(currentDay);

        NewQuestCreator questCreator = new NewQuestCreator(_saveLoadSystem, currentDay);
        _questAcceptingMonitor = new(_newQuestInitializer);
        _newQuestInitializer.Init(questCreator.NewQuests, _eventsConfiguration, _peasants);
        _table = new Table(_saveLoadSystem, _newQuestInitializer, _deskInitializer, currentDay, _eventsConfiguration);
        _desk = new Desk(_saveLoadSystem, _newQuestInitializer, _tableInitializer, currentDay, _eventsConfiguration);

        _tableInitializer.Init(_table, _eventsConfiguration);
        _deskInitializer.Init(_desk, _eventsConfiguration);

        //*******************Delete***************
        _resetButton.onClick.AddListener(PlayerPrefs.DeleteAll);
        //***********************************************

        _dayView.Init(currentDay);

        if (currentDay == Days.Sunday)
            _nextDayButton.gameObject.SetActive(false);

        _nextDayButton.onClick.AddListener(OnNextDayButtonClick);
        _questAcceptingMonitor.AllQuestsHandled += OnAllQuestHandled;

        _mood = _saveLoadSystem.GetMood();
        _healthView.Init(_mood);

        if (_mood == Health.Riot)
            _loseScreen.Enable();
    }

    private void OnDestroy()
    {
        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClick);
        _questAcceptingMonitor.AllQuestsHandled -= OnAllQuestHandled;
    }

    private void OnNextDayButtonClick()
    {
        _dayData.SaveDay();
        _table.SaveData();
        _desk.SaveData();
        _saveLoadSystem.SaveMood(_mood);
        SceneManager.LoadScene(Scenes.Consequences.ToString());
    }

    private void OnAllQuestHandled()
    {
        _nextDayButton.interactable = true;
    }
}
