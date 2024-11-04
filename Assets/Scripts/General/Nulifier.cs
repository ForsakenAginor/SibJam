using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Nulifier : MonoBehaviour
{
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
        PlayerPrefs.DeleteKey("AvailableQuests");
        PlayerPrefs.DeleteKey("StoredQuests");
        PlayerPrefs.DeleteKey("PlacedQuests");
        PlayerPrefs.DeleteKey("Mood");
        PlayerPrefs.DeleteKey("DayDataKey");
    }
}
