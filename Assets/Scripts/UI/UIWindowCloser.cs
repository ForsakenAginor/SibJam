using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIWindowCloser : MonoBehaviour
{
    [SerializeField] private UIElement _currentWindow;

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

    private void OnButtonClick()
    {
        _currentWindow.Disable();
    }
}