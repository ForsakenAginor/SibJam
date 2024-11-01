using TMPro;
using UnityEngine;

public class DayView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    public void Init(Days currentDay)
    {
        _textField.text = currentDay.ToString();
    }
}
