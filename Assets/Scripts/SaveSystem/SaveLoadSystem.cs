using System.Collections.Generic;

public class SaveLoadSystem
{
    private const string AvailableQuests = nameof(AvailableQuests);
    private const string StoredQuests = nameof(StoredQuests);
    private const string PlacedQuests = nameof(PlacedQuests);

    private readonly DataStorage<List<EventNames>> _availableQuests;
    private readonly DataStorage<List<SerializableQuest>> _storedQuests;
    private readonly DataStorage<List<SerializableQuest>> _placedQuests;

    public SaveLoadSystem(Days currentDay)
    {
        _availableQuests = new DataStorage<List<EventNames>>(AvailableQuests, currentDay);
        _storedQuests = new DataStorage<List<SerializableQuest>>(StoredQuests, currentDay);
        _placedQuests = new DataStorage<List<SerializableQuest>>(PlacedQuests, currentDay);
    }

    public List<EventNames> GetAvailableQuests() => _availableQuests.LoadData();

    public void SaveAvailableQuests(List<EventNames> quests) => _availableQuests.SaveData(quests);

    public List<SerializableQuest> GetStoredQuests() => _storedQuests.LoadData();

    public void SaveStoredQuests(List<SerializableQuest> quests) => _storedQuests.SaveData(quests);

    public List<SerializableQuest> GetPlacedQuests() => _placedQuests.LoadData();

    public void SavePlacedQuests(List<SerializableQuest> quests) => _placedQuests.SaveData(quests);
}