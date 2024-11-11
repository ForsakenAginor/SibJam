using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Scripts.UnityAnalytics
{
    public class AnalyticHarvester
    {
        public void CreateLog(AnalyticsEvent analyticsEvent)
        {
            AnalyticsService.Instance.RecordEvent(analyticsEvent.ToString());
            //AnalyticsResult result = Analytics.CustomEvent(analyticsEvent.ToString());
            //Debug.Log($"Analytic log: {result}");
        }
    }
}