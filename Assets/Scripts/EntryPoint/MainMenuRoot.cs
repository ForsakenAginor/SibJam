using Sound;
using UnityEngine;

public class MainMenuRoot : MonoBehaviour
{
    [SerializeField] private SoundInitializer _soundInitializer;

    private void Start()
    {
        _soundInitializer.Init();
        _soundInitializer.AddMusicSource(Singleton.Instance.Music);
    }
}
