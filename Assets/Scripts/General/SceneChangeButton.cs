using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.General
{
    [RequireComponent(typeof(Button))]
    public class SceneChangeButton : MonoBehaviour
    {
        [SerializeField] private Scenes _scene;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
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
            Invoke("MainScene", 0.5f);
        }

    }
}