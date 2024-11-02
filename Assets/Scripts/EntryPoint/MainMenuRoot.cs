using Sound;
using UnityEngine;

public class MainMenuRoot : MonoBehaviour
{
    [SerializeField] private SoundInitializer _soundInitializer;
    [SerializeField] private AudioSource _ambientMusic;

    private void Start()
    {
        _soundInitializer.Init();
    }
}
