using System.Collections.Generic;

public class SaveLoadSystem
{
    private const string AvailableQuests = nameof(AvailableQuests);
    private const string StoredQuests = nameof(StoredQuests);
    private const string PlacedQuests = nameof(PlacedQuests);
    private readonly DataStorage<List<EventNames>> _availableQuests;

    public SaveLoadSystem(Days currentDay)
    {
        _availableQuests = new DataStorage<List<EventNames>>(AvailableQuests, currentDay);
    }

    public List<EventNames> GetAvailableQuests() => _availableQuests.LoadData();

    public void SaveAvailableQuests(List<EventNames> quests) => _availableQuests.SaveData(quests);
}
