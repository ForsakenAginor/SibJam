using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EventsConfiguration")]
public class EventsConfiguration : UpdatableConfiguration<EventNames, VillageEventSO>, IEventsInfoGetter
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