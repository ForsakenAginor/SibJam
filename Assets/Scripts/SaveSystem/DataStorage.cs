using System;
using Newtonsoft.Json;

namespace Assets.Scripts.SaveSystem
{
    public class DataStorage<T>
    {
        private readonly IStringSaveLoadService _saveLoadService;
        private readonly bool _isFirstCreation;

        public DataStorage(string keyWord, bool isFirstCreation)
        {
            if (string.IsNullOrEmpty(keyWord))
                throw new ArgumentNullException(nameof(keyWord));

            _saveLoadService = new PlayerPrefsStringSaveLoadService(keyWord);
            _isFirstCreation = isFirstCreation;
        }

        public T LoadData()
        {
            T result = default;

            if (_isFirstCreation == false)
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
}