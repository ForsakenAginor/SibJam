namespace Assets.Scripts.EventConfiguration
{
    public interface IEventsLifeTimeInfoGetter
    {
        public int GetLifeTime(EventNames name);
    }
}