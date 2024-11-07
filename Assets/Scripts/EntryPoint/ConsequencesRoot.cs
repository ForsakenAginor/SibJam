﻿using Assets.Scripts.EventConfiguration;
using Assets.Scripts.General;
using Assets.Scripts.Quests;
using Assets.Scripts.SaveSystem;
using Assets.Scripts.Sound.AudioMixer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsequencesRoot : MonoBehaviour
{
    [SerializeField] private SoundInitializer _soundInitializer;
    [SerializeField] private EventsConfiguration _eventsConfiguration;

    [SerializeField] private ConsequencesCardsShower _consequencesCardsShower;
    [SerializeField] private DeskViewChanger _deskViewChanger;
    [SerializeField] private HeroAnimation _heroAnimation;
    [SerializeField] private float _animationDuration = 5f;

    private HealthDamageSystem _healthDamageSystem;

    private void Start()
    {
        _soundInitializer.Init();
        _soundInitializer.AddMusicSourceWithoutVolumeChanging(MusicSingleton.Instance.Music);

        SaveLoadSystem saveLoadSystem = new ();
        Days currentDay = saveLoadSystem.GetCurrentDay();

        List<Quest> expiredQuests = new List<Quest>();
        List<SerializableQuest> quests = new List<SerializableQuest>();

        List<SerializableQuest> savedQuests = saveLoadSystem.GetStoredQuests();
        ExpireQuests(currentDay, expiredQuests, quests, savedQuests);
        saveLoadSystem.SaveStoredQuests(quests);

        savedQuests = saveLoadSystem.GetPlacedQuests();
        _deskViewChanger.Init(savedQuests.Count);

        RandomQuestPicker questPicker = new RandomQuestPicker();
        Quest chosedQuest = questPicker.ChoseRandomQuest(savedQuests);
        quests.Clear();
        ExpireQuests(currentDay, expiredQuests, quests, savedQuests);
        saveLoadSystem.SavePlacedQuests(quests);
        _heroAnimation.Init(_animationDuration);

        _consequencesCardsShower.AllEventsShown += OnAllEventsShown;
        _healthDamageSystem = new HealthDamageSystem(_consequencesCardsShower, _eventsConfiguration, saveLoadSystem);

        StartCoroutine(WaitAnimationPlayed(expiredQuests, chosedQuest));
        SceneChangerSingleton.Instance.FadeOut();
    }

    private void OnDestroy()
    {
        _consequencesCardsShower.AllEventsShown -= OnAllEventsShown;
    }

    private IEnumerator WaitAnimationPlayed(List<Quest> expiredQuests, Quest chosedQuest)
    {
        WaitForSeconds delay = new WaitForSeconds(_animationDuration);
        yield return delay;
        _deskViewChanger.ChangeDesk();
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
        SceneChangerSingleton.Instance.LoadScene(Scenes.GameScene.ToString());
    }
}