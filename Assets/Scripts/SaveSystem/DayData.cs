using Assets.Scripts.DaySystem;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class DayData
    {
        private const string DayDataKey = nameof(DayDataKey);

        private int _day;

        public DayData()
        {
            if (PlayerPrefs.HasKey(DayDataKey))
                _day = PlayerPrefs.GetInt(DayDataKey);
        }

        public Days GetCurrentDay()
        {
            return (Days)_day;
        }

        public void SaveDay()
        {
            _day++;
            PlayerPrefs.SetInt(DayDataKey, _day);
        }

        public void RemoveData()
        {
            PlayerPrefs.DeleteKey(DayDataKey);
        }
    }
}