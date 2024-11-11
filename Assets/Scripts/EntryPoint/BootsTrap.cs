using Assets.Scripts.General;
using Localization;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootsTrap : MonoBehaviour
{
    private async void Start()
    {
        LocalizationInitializer localization = new LocalizationInitializer();

        localization.ApplyLocalization("en");
        await UnityServices.InitializeAsync();
        Debug.Log("Analytic initialized");
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log("Analytic data collection start");
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }
}
