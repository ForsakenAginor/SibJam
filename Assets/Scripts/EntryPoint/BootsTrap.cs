using Assets.Scripts.General;
using Assets.Scripts.UnityAnalytics;
using Localization;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private AnalyticGameObject _analyticsService;

    private async void Start()
    {
        LocalizationInitializer localization = new LocalizationInitializer();

        localization.ApplyLocalization("en");
        await _analyticsService.Init();
        //InitializeServices();
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }

    private async void InitializeServices()
    {
        await UnityServices.InitializeAsync();
    }
}
