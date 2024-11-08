using Assets.Scripts.General;
using Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootsTrap : MonoBehaviour
{
    private void Start()
    {
        LocalizationInitializer localization = new LocalizationInitializer();
        localization.ApplyLocalization("en");
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }
}
