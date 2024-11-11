using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace Assets.Scripts.UnityAnalytics
{
    public class AnalyticGameObject : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public async Task Init()
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Analytic initialized");
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("Analytic data collection start");
        }
    }
}