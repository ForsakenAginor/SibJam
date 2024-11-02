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

    [SerializeField] private float _duration;

    private void Start()
    {
        _soundInitializer.Init();
        DayData dayData = new DayData();
        Days currentDay = dayData.GetCurrentDay();
        SaveLoadSystem saveLoadSystem = new SaveLoadSystem(currentDay);
        List<Quest> expiredQuests = new List<Quest>();
        ExpireQuest(currentDay, saveLoadSystem, expiredQuests);
        _consequencesCardsShower.AllEventsShown += OnAllEventsShown;
        StartCoroutine(WaitAnimationPlayed(expiredQuests));
        //TODO ****************************Create another list with completed quests
        //TODO ****************************Add list of completed by Hero quests to _consequencesCardsShower
    }


    private void OnDestroy()
    {
        _consequencesCardsShower.AllEventsShown -= OnAllEventsShown;
    }

    private IEnumerator WaitAnimationPlayed(List<Quest> expiredQuests)
    {
        WaitForSeconds delay = new WaitForSeconds(_duration);
        yield return delay;
        _consequencesCardsShower.Init(expiredQuests, _eventsConfiguration);
    }

    private void ExpireQuest(Days currentDay, SaveLoadSystem saveLoadSystem, List<Quest> expiredQuests)
    {
        List<SerializableQuest> questsInTable = new List<SerializableQuest>();
        List<SerializableQuest> questsInDesk = new List<SerializableQuest>();
        List<SerializableQuest> savedQuests = saveLoadSystem.GetStoredQuests();

        foreach (var item in savedQuests)
        {
            Quest quest = new(item.EventName, item.DayObtain);
            quest.CalcExpireDate(currentDay, _eventsConfiguration.GetLifeTime(quest.EventName));

            if (quest.DaysToExpire == 0)
                expiredQuests.Add(quest);
            else
                questsInTable.Add(item);
        }

        saveLoadSystem.SaveStoredQuests(questsInTable);
        savedQuests = saveLoadSystem.GetPlacedQuests();

        foreach (var item in savedQuests)
        {
            Quest quest = new(item.EventName, item.DayObtain);
            quest.CalcExpireDate(currentDay, _eventsConfiguration.GetLifeTime(quest.EventName));

            if (quest.DaysToExpire == 0)
                expiredQuests.Add(quest);
            else
                questsInDesk.Add(item);
        }

        saveLoadSystem.SavePlacedQuests(questsInDesk);
    }

    private void OnAllEventsShown()
    {
        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }
}