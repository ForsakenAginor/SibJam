public class Quest
{
    private readonly EventNames _eventName;
    private readonly Days _dayObtain;

    public Quest(EventNames eventName, Days dayObtain)
    {
        _eventName = eventName;
        _dayObtain = dayObtain;
    }

    public EventNames EventName => _eventName;

    public Days DayObtain => _dayObtain;
}
