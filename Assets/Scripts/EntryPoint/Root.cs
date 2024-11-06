using Assets.Scripts.Quests;
using Assets.Scripts.Quests.QuestCreation;
using Assets.Scripts.Quests.QuestCreation.View;
using Assets.Scripts.Quests.Storage;
using Assets.Scripts.Quests.Storage.View;
using Assets.Scripts.SaveSystem;
using Sound;
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
    [SerializeField] private StorageViewCreator _bagViewCreator;
    [SerializeField] private StorageViewCreator _deskViewCreator;
    [SerializeField] private List<InteractablePeasant> _peasants;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private MoodInfoView _moodInfoView;
    [SerializeField] private BugSpriteChanger _bugSpriteChanger;
    [SerializeField] private DeskSpriteChanger _deskSpriteChanger;
    [SerializeField] private UIElement _tutorial;

    private QuestStorage _table;
    private QuestStorage _desk;
    private QuestAcceptingMonitor _questAcceptingMonitor;
    private SaveLoadSystem _saveLoadSystem;
    private Health _mood;

    private void Start()
    {
        _soundInitializer.Init();
        _soundInitializer.AddMusicSourceWithoutVolumeChanging(MusicSingleton.Instance.Music);
        _nextDayButton.interactable = false;

        _saveLoadSystem = new SaveLoadSystem();
        Days currentDay = _saveLoadSystem.GetCurrentDay();
        _mood = _saveLoadSystem.GetMood();

        _healthView.Init(_mood);

        if(currentDay == Days.Monday)
            _tutorial.Enable();

        if (_mood == Health.Riot)
        {
            SceneManager.LoadScene(Scenes.LoseScene.ToString());
            return;
        }
        else if (currentDay == Days.Final)
        {
            SceneManager.LoadScene(Scenes.WinScene.ToString());
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
        DeskAdapter deskAdapter = new DeskAdapter(_newQuestInitializer);

        _table = new QuestStorage(bagAdapter, _deskViewCreator, currentDay, _eventsConfiguration,
            _saveLoadSystem.GetStoredQuests(), _saveLoadSystem.SaveStoredQuests);
        _desk = new QuestStorage(deskAdapter, _bagViewCreator, currentDay, _eventsConfiguration,
            _saveLoadSystem.GetPlacedQuests(), _saveLoadSystem.SavePlacedQuests);

        _bugSpriteChanger.Init(_table);
        _deskSpriteChanger.Init(_desk);
        _bagViewCreator.Init(_table, _eventsConfiguration);
        _deskViewCreator.Init(_desk, _eventsConfiguration);

        _dayView.Init(currentDay);

        _nextDayButton.onClick.AddListener(OnNextDayButtonClick);
        _questAcceptingMonitor.AllQuestsHandled += OnAllQuestHandled;

    }

    private void OnDestroy()
    {
        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClick);
        _questAcceptingMonitor.AllQuestsHandled -= OnAllQuestHandled;
    }

    private void OnNextDayButtonClick()
    {
        _saveLoadSystem.SaveDay();
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
