using Sound;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [SerializeField] private EventsConfiguration _eventsConfiguration;
    [SerializeField] private SoundInitializer _soundInitializer;

    [SerializeField] private DayView _dayView;
    [SerializeField] private Button _nextDayButton;

    private DayData _dayData;

    private void Start()
    {
        _soundInitializer.Init();
        _dayData = new DayData();
        Days currentDay = _dayData.GetCurrentDay();

        if(currentDay == Days.Sunday)
            _nextDayButton.gameObject.SetActive(false);

        _dayView.Init(currentDay);

        _nextDayButton.onClick.AddListener(OnNextDayButtonClick);
    }

    private void OnDestroy()
    {
        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClick);
    }

    private void OnNextDayButtonClick()
    {
        _dayData.SaveDay();
        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }
}
