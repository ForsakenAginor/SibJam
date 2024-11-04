using Sound;
using UnityEngine;

public class MainMenuRoot : MonoBehaviour
{
    [SerializeField] private SoundInitializer _soundInitializer;

    private void Start()
    {
        _soundInitializer.Init();

        if (Singleton.Instance.IsAdded == false)
            _soundInitializer.AddMusicSource(Singleton.Instance.Music);
        else
            _soundInitializer.AddMusicSourceWithoutVolumeChanging(Singleton.Instance.Music);
    }
}
