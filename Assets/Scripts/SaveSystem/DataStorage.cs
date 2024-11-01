using System;
using Newtonsoft.Json;

public class DataStorage<T>
{
    private readonly IStringSaveLoadService _saveLoadService;
    private readonly Days _currentDay;

    public DataStorage(string keyWord, Days currentDay)
    {
        if (string.IsNullOrEmpty(keyWord))
            throw new ArgumentNullException(nameof(keyWord));

        _saveLoadService = new PlayerPrefsStringSaveLoadService(keyWord);
        _currentDay = currentDay;
    }

    public T LoadData()
    {
        T result = default;

        if (_currentDay != Days.Monday)
        {
            string data = _saveLoadService.GetSavedInfo();
            result = JsonConvert.DeserializeObject<SerializableT>(data).Content;
        }

        return result;
    }

    public void SaveData(T dataThatWillBeSaved)
    {
        SerializableT serializableT = new SerializableT()
        {
            Content = dataThatWillBeSaved,
        };
        string data = JsonConvert.SerializeObject(
            serializableT,
            Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            );

        _saveLoadService.SaveInfo(data);
    }

    [Serializable]
    private class SerializableT
    {
        public T Content;
    }        
}