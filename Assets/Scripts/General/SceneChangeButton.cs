using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] private Scenes _scene;
    private Button _button;
    [SerializeField] private GameObject BS;
    private Animator _animator;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
        _animator = BS.GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);        
    }
    public void MainScene()
    {
        SceneManager.LoadScene(_scene.ToString());
    }

    private void OnButtonClick()
    {
        Invoke("MainScene", 1f);
    }

}
