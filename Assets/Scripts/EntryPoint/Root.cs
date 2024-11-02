using Sound;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private DayData _dayData;
    private Table _table;

    //*******************Delete***************
    [Header("TestOnly")]
    [SerializeField] private Button _newQuestTestButton;
    [SerializeField] private Button _resetButton;
    private List<Quest> _newQuests;
    //****************************************

    private void Start()
    {
        _soundInitializer.Init();
        _dayData = new DayData();
        Days currentDay = _dayData.GetCurrentDay();
        SaveLoadSystem saveLoadSystem = new SaveLoadSystem(currentDay);
        NewQuestCreator questCreator = new NewQuestCreator(saveLoadSystem, currentDay);
        _newQuestInitializer.Init(questCreator.NewQuests, _eventsConfiguration);
        _table = new Table(saveLoadSystem, _newQuestInitializer, currentDay, _eventsConfiguration);
        _tableInitializer.Init(_table, _eventsConfiguration);

        //*******************Delete***************
        _newQuests = questCreator.NewQuests;
        _newQuestTestButton.onClick.AddListener(OnNewQuestTestClick);
        _resetButton.onClick.AddListener(PlayerPrefs.DeleteAll);
        //***********************************************

        _dayView.Init(currentDay);

        if(currentDay == Days.Sunday)
            _nextDayButton.gameObject.SetActive(false);

        _nextDayButton.onClick.AddListener(OnNextDayButtonClick);
    }

    private void OnDestroy()
    {
        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClick);
    }

    //*******************Delete***************
    private void OnNewQuestTestClick()
    {
        foreach (var item in _newQuests)
            Debug.Log($"{item.EventName} {item.DayObtain}");
    }
    //*********************


    private void OnNextDayButtonClick()
    {
        _dayData.SaveDay();
        _table.SaveData();
        SceneManager.LoadScene(Scenes.Consequences.ToString());
    }
}
