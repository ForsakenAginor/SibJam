using System;

namespace Assets.Scripts.SaveSystem
{
    [Serializable]
    public struct SerializableQuest
    {
        public EventNames EventName;
        public Days DayObtain;
    }
}