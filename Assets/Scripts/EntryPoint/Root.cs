using Assets.Scripts.EventConfiguration;
using Assets.Scripts.General;
using Assets.Scripts.Mood;
using Assets.Scripts.Quests;
using Assets.Scripts.Quests.QuestCreation;
using Assets.Scripts.Quests.QuestCreation.View;
using Assets.Scripts.Quests.Storage;
using Assets.Scripts.Quests.Storage.View;
using Assets.Scripts.SaveSystem;
using Assets.Scripts.Sound.AudioMixer;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EventsConfiguration _eventsConfiguration;
    private SaveLoadSystem _saveLoadSystem;

    [Header("Audio")]
    [SerializeField] private SoundInitializer _soundInitializer;

    [Header("Desk")]
    [SerializeField] private StorageViewCreator _deskViewCreator;
    [SerializeField] private DeskSpriteChanger _deskSpriteChanger;
    private QuestStorage _desk;

    [Header("Bag")]
    [SerializeField] private StorageViewCreator _bagViewCreator;
    [SerializeField] private BagSpriteChanger _bugSpriteChanger;
    private QuestStorage _bag;

    [Header("QuestGivingSystem")]
    [SerializeField] private NewQuestInitializer _newQuestInitializer;
    [SerializeField] private List<InteractableSprite> _peasants;

    [Header("HealthSystem")]
    [SerializeField] private MoodInfoView _moodInfoView;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private Button _moodInfoAcceptButton;
    private Health _mood;

    [Header("GameProgress")]
    [SerializeField] private DayView _dayView;
    [SerializeField] private UIElement _tutorial;
    [SerializeField] private Button _nextDayButton;
    [SerializeField] private Button _tutorialAcceptButton;
    private QuestAcceptingMonitor _questAcceptingMonitor;
    bool _isFinished = false;

    private void Start()
    {
        _soundInitializer.Init();
        _soundInitializer.AddMusicSourceWithoutVolumeChanging(MusicSingleton.Instance.Music);
        _saveLoadSystem = new SaveLoadSystem();
        _nextDayButton.interactable = false;

        Days currentDay = _saveLoadSystem.GetCurrentDay();
        _mood = _saveLoadSystem.GetMood();
        _healthView.Init(_mood);
        _dayView.Init(currentDay);

        if(currentDay == Days.Monday)
            _tutorial.Enable();

        if (_mood == Health.Riot)
        {
            SceneManager.LoadScene(Scenes.LoseScene.ToString());
            _isFinished = true;
            return;
        }
        else if (currentDay == Days.Final)
        {
            SceneManager.LoadScene(Scenes.WinScene.ToString());
            _isFinished = true;
            return;
        }
        else if (currentDay != Days.Monday)
        {
            _moodInfoView.ShowMoodInfo(_mood);
        }

        NewQuestCreator questCreator = new NewQuestCreator(_saveLoadSystem, currentDay);
        _questAcceptingMonitor = new (_newQuestInitializer);
        _newQuestInitializer.Init(questCreator.NewQuests, _eventsConfiguration, _peasants);

        BagAdapter bagAdapter = new BagAdapter(_newQuestInitializer);
        _bag = new QuestStorage(bagAdapter, _deskViewCreator, currentDay, _eventsConfiguration,
            _saveLoadSystem.GetStoredQuests(), _saveLoadSystem.SaveStoredQuests);

        DeskAdapter deskAdapter = new DeskAdapter(_newQuestInitializer);
        _desk = new QuestStorage(deskAdapter, _bagViewCreator, currentDay, _eventsConfiguration,
            _saveLoadSystem.GetPlacedQuests(), _saveLoadSystem.SavePlacedQuests);

        _bugSpriteChanger.Init(_bag);
        _deskSpriteChanger.Init(_desk);
        _bagViewCreator.Init(_bag, _eventsConfiguration);
        _deskViewCreator.Init(_desk, _eventsConfiguration);

        SceneChangerSingleton.Instance.FadeOut();

        _nextDayButton.onClick.AddListener(OnNextDayButtonClick);
        _questAcceptingMonitor.AllQuestsHandled += OnAllQuestHandled;
        _moodInfoAcceptButton.onClick.AddListener(OnEntryMessageAccept);
        _tutorialAcceptButton.onClick.AddListener(OnEntryMessageAccept);
    }


    private void OnDestroy()
    {
        if(_isFinished)
            return;

        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClick);
        _questAcceptingMonitor.AllQuestsHandled -= OnAllQuestHandled;
    }

    private void OnEntryMessageAccept()
    {
        foreach(var peasant in _peasants)
            peasant.PlayPulseAnimation();
    }

    private void OnNextDayButtonClick()
    {
        _saveLoadSystem.SaveDay();
        _bag.SaveData();
        _desk.SaveData();
        _saveLoadSystem.SaveMood(_mood);
        SceneChangerSingleton.Instance.LoadScene(Scenes.Consequences.ToString());
    }

    private void OnAllQuestHandled()
    {
        _nextDayButton.interactable = true;
    }
}
