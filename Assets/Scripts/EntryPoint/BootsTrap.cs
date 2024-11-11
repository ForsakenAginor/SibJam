using Assets.Scripts.General;
using Assets.Scripts.UI;
using Localization;
using System.Collections;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _declineButton;
    [SerializeField] private UIElement _analyticWindow;
    [SerializeField] private UIElement _logoWindow;
    [SerializeField] private float _gameStartDelay = 2f;

    private async void Start()
    {
        LocalizationInitializer localization = new LocalizationInitializer();

        localization.ApplyLocalization("en");
        await UnityServices.InitializeAsync();
        Debug.Log("Analytic initialized");

        _acceptButton.onClick.AddListener(OnAccept);
        _declineButton.onClick.AddListener(OnDecline);
    }

    private void OnDecline()
    {
        AnalyticsService.Instance.StopDataCollection();
        Debug.Log("Analytic data collection stopped");
        AnalyticsService.Instance.RequestDataDeletion();
        Debug.Log("Analytic data deletion request was sent");
        StartCoroutine(ShowLogo());
    }

    private void OnAccept()
    {
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log("Analytic data collection start");
        StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        WaitForSeconds delay = new WaitForSeconds(_gameStartDelay);
        _analyticWindow.Disable();
        _logoWindow.Enable();
        yield return delay;
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }
}
