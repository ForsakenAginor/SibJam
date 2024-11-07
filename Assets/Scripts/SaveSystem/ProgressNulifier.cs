using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class ProgressNulifier : MonoBehaviour
    {
        private void Start()
        {
            SaveLoadSystem saveLoadSystem = new SaveLoadSystem();
            saveLoadSystem.RemoveDayData();
        }
    }
}