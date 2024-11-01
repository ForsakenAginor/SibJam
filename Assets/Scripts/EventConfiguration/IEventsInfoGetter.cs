using UnityEngine;

public interface IEventsInfoGetter
{
    public string GetName(EventNames name);

    public string GetDescription(EventNames name);

    public string GetFailDescription(EventNames name);

    public string GetSuccessDescription(EventNames name);

    public Sprite GetSuccessSprite(EventNames name);

    public Sprite GetFailureSprite(EventNames name);
}
