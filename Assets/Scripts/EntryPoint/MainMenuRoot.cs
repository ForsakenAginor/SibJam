using Assets.Scripts.General;
using Assets.Scripts.Sound.AudioMixer;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.EntryPoint
{
    public class MainMenuRoot : MonoBehaviour
    {
        [SerializeField] private SoundInitializer _soundInitializer;
        [SerializeField] private Button _toEnglish;
        [SerializeField] private Button _toRussian;

        private void Start()
        {
            _soundInitializer.Init();

            if (MusicSingleton.Instance.IsAdded == false)
                _soundInitializer.AddMusicSource(MusicSingleton.Instance.Music);
            else
                _soundInitializer.AddMusicSourceWithoutVolumeChanging(MusicSingleton.Instance.Music);

            SceneChangerSingleton.Instance.FadeOut();

            _toEnglish.onClick.AddListener(OnToEnglishButtonClick);
            _toRussian.onClick.AddListener(OnToRussianButtonClick);
        }

        private void OnDestroy()
        {
            _toEnglish.onClick.RemoveListener(OnToEnglishButtonClick);
            _toRussian.onClick.RemoveListener(OnToRussianButtonClick);            
        }

        private void OnToRussianButtonClick()
        {
            const string Russian = nameof(Russian);

            LeanLocalization.SetCurrentLanguageAll(Russian);
            LeanLocalization.UpdateTranslations();
        }

        private void OnToEnglishButtonClick()
        {
            const string English = nameof(English);

            LeanLocalization.SetCurrentLanguageAll(English);
            LeanLocalization.UpdateTranslations();
        }
    }
}