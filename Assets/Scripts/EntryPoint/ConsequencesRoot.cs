using Sound;
using System;
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
        List<SerializableQuest> quests = new List<SerializableQuest>();

        List<SerializableQuest> savedQuests = saveLoadSystem.GetStoredQuests();
        ExpireQuests(currentDay, expiredQuests, quests, savedQuests);
        saveLoadSystem.SaveStoredQuests(quests);

        savedQuests = saveLoadSystem.GetPlacedQuests();
        RandomQuestPicker questPicker = new RandomQuestPicker();
        Quest chosedQuest = questPicker.ChoseRandomQuest(savedQuests);
        quests.Clear();
        ExpireQuests(currentDay, expiredQuests, quests, savedQuests);
        saveLoadSystem.SavePlacedQuests(quests);
        _consequencesCardsShower.AllEventsShown += OnAllEventsShown;

        StartCoroutine(WaitAnimationPlayed(expiredQuests, chosedQuest));
        //TODO ****************************Create another list with completed quests
        //TODO ****************************Add list of completed by Hero quests to _consequencesCardsShower
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
        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }
}

public class RandomQuestPicker
{
    public Quest ChoseRandomQuest(List<SerializableQuest> quests)
    {
        if(quests == null)
            throw new ArgumentNullException(nameof(quests));
        
        if (quests.Count == 0)
            return null;

        int index = UnityEngine.Random.Range(0, quests.Count);
        Quest quest = new Quest(quests[index].EventName, quests[index].DayObtain);
        quests.RemoveAt(index);
        return quest;
    }
}