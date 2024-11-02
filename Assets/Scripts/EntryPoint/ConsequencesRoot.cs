using Sound;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsequencesRoot : MonoBehaviour
{
    [SerializeField] private SoundInitializer _soundInitializer;
    [SerializeField] private EventsConfiguration _eventsConfiguration;
    [SerializeField] private ConsequencesCardsShower _consequencesCardsShower;

    private void Start()
    {
        _soundInitializer.Init();
        DayData dayData = new DayData();
        Days currentDay = dayData.GetCurrentDay();
        SaveLoadSystem saveLoadSystem = new SaveLoadSystem(currentDay);
        List<Quest> expiredQuests = new List<Quest>();
        List<SerializableQuest> quests = new List<SerializableQuest>();
        List<SerializableQuest> savedQuests = saveLoadSystem.GetStoredQuests();

        //TODO**************************ADD QUESTS FROM BOARD******************
        foreach (var item in savedQuests)
        {
            Quest quest = new(item.EventName, item.DayObtain);
            quest.CalcExpireDate(currentDay, _eventsConfiguration.GetLifeTime(quest.EventName));

            if (quest.DaysToExpire == 0)
                expiredQuests.Add(quest);
            else
                quests.Add(item);
        }

        //TODO ****************************Create another list with completed quests
        saveLoadSystem.SaveStoredQuests(quests);
        _consequencesCardsShower.AllEventsShown += OnAllEventsShown;

        //TODO ****************************Add list of completed by Hero quests to _consequencesCardsShower
        _consequencesCardsShower.Init(expiredQuests, _eventsConfiguration);
    }

    private void OnDestroy()
    {
        _consequencesCardsShower.AllEventsShown -= OnAllEventsShown;
    }

    private void OnAllEventsShown()
    {
        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }
}