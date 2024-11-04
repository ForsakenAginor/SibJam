using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton _instance;
    [SerializeField] private AudioSource _music;
    private bool _isAdded;

    public static Singleton Instance { get { return _instance; } }

    public AudioSource Music
    {
        get
        {
            _isAdded = true;
            return _music;
        }
    }    

    public bool IsAdded => _isAdded;

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