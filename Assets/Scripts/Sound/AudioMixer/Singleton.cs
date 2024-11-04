using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    private static Singleton _instance;

    public static Singleton Instance { get { return _instance; } }

    public AudioSource Music => _music;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}