using Assets.Scripts.DaySystem;
using Assets.Scripts.EventConfiguration;
using Assets.Scripts.SaveSystem;

namespace Assets.Scripts.Quests
{
    public class Quest
    {
        private readonly EventNames _eventName;
        private readonly Days _dayObtain;
        private int _daysToExpire;

        public Quest(EventNames eventName, Days dayObtain)
        {
            _eventName = eventName;
            _dayObtain = dayObtain;
        }

        public EventNames EventName => _eventName;

        public Days DayObtain => _dayObtain;

        public int DaysToExpire => _daysToExpire;

        public void CalcExpireDate(Days currentDay, int lifeTime)
        {
            _daysToExpire = (int)_dayObtain + lifeTime - (int)currentDay;
        }

        public SerializableQuest Serialize()
        {
            SerializableQuest quest = new SerializableQuest();
            quest.EventName = _eventName;
            quest.DayObtain = _dayObtain;
            return quest;
        }
    }
}