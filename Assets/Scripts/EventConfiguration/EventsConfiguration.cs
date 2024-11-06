using Assets.Scripts.EventConfiguration.Abstraction;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.EventConfiguration
{
    [CreateAssetMenu(fileName = "EventsConfiguration")]
    public class EventsConfiguration : UpdatableConfiguration<EventNames, VillageEventSO>,
        IEventsInfoGetter, IEventsLifeTimeInfoGetter, IImportantInfoGetter
    {
        public string GetDescription(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.Description;
        }

        public string GetFailDescription(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.FailDescription;
        }

        public Sprite GetFailureSprite(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.FailSprite;
        }

        public bool GetImportantStatus(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.IsImportant;
        }

        public int GetLifeTime(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.Deadline;
        }

        public string GetName(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.Name;
        }

        public string GetSuccessDescription(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.CompleteDescription;
        }

        public Sprite GetSuccessSprite(EventNames name)
        {
            return Content.First(o => o.Key == name).Value.CompleteSprite;
        }
    }
}