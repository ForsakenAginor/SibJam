using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsequencesRoot : MonoBehaviour
{
    [SerializeField] private SoundInitializer _soundInitializer;
    [SerializeField] private EventsConfiguration _eventsConfiguration;
    [SerializeField] private ConsequencesCardsShower _consequencesCardsShower;
    [SerializeField] private DeskSpriteChanger _deskSpriteChanger;
    [SerializeField] private GameObject Black_screen;

    [SerializeField] private float _duration;

    private HealthDamageSystem _healthDamageSystem;

    private void Start()
    {
        _soundInitializer.Init();
        DayData dayData = new DayData();
        Days currentDay = dayData.GetCurrentDay();
        SaveLoadSystem saveLoadSystem = new SaveLoadSystem(currentDay);

        List<Quest> expiredQuests = new List<Quest>();
        List<SerializableQuest> quests = new List<SerializableQuest>();

        List<SerializableQuest> savedQuests = saveLoadSystem.GetStoredQuests();
        ExpireQuests(currentDay, expiredQuests, quests, savedQuests);
        saveLoadSystem.SaveStoredQuests(quests);

        savedQuests = saveLoadSystem.GetPlacedQuests();
        _deskSpriteChanger.Init(savedQuests.Count);
        RandomQuestPicker questPicker = new RandomQuestPicker();
        Quest chosedQuest = questPicker.ChoseRandomQuest(savedQuests);
        quests.Clear();
        ExpireQuests(currentDay, expiredQuests, quests, savedQuests);
        saveLoadSystem.SavePlacedQuests(quests);
        _consequencesCardsShower.AllEventsShown += OnAllEventsShown;
        _healthDamageSystem = new HealthDamageSystem(_consequencesCardsShower, _eventsConfiguration, saveLoadSystem);

        StartCoroutine(WaitAnimationPlayed(expiredQuests, chosedQuest));
    }

    private void OnDestroy()
    {
        _consequencesCardsShower.AllEventsShown -= OnAllEventsShown;
    }

    private IEnumerator WaitAnimationPlayed(List<Quest> expiredQuests, Quest chosedQuest)
    {
        WaitForSeconds delay = new WaitForSeconds(_duration);
        yield return delay;
        _consequencesCardsShower.Init(expiredQuests, chosedQuest, _eventsConfiguration);
    }

    private void ExpireQuests(Days currentDay, List<Quest> expiredQuests,
        List<SerializableQuest> quests, List<SerializableQuest> savedQuests)
    {
        foreach (var item in savedQuests)
        {
            Quest quest = new(item.EventName, item.DayObtain);
            quest.CalcExpireDate(currentDay, _eventsConfiguration.GetLifeTime(quest.EventName));

            if (quest.DaysToExpire == 0)
                expiredQuests.Add(quest);
            else
                quests.Add(item);
        }
    }

    private void OnAllEventsShown()
    {
        _healthDamageSystem.SaveMood();
        Black_screen.GetComponent<Black_Screen_start>().Anim();
        StartCoroutine(waitmethod());
    }

    public IEnumerator waitmethod()
    {
        WaitForSeconds time = new WaitForSeconds(1f);
        yield return time;
        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }
}
