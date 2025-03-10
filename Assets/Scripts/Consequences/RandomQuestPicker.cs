﻿using Assets.Scripts.Quests;
using Assets.Scripts.SaveSystem;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Consequences
{
    public class RandomQuestPicker
    {
        public Quest ChoseRandomQuest(List<SerializableQuest> quests)
        {
            if (quests == null)
                throw new ArgumentNullException(nameof(quests));

            if (quests.Count == 0)
                return null;

            int index = UnityEngine.Random.Range(0, quests.Count);
            Quest quest = new Quest(quests[index].EventName, quests[index].DayObtain);
            quests.RemoveAt(index);
            return quest;
        }
    }
}