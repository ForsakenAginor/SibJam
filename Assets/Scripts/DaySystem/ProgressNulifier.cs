using UnityEngine;

public class ProgressNulifier : MonoBehaviour
{
    private void Start()
    {
        const string DayDataKey = nameof(DayDataKey);

        if (PlayerPrefs.HasKey(DayDataKey))
            PlayerPrefs.DeleteKey(DayDataKey);
    }
}
