using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    Dictionary<Days, string> Dayss = new Dictionary<Days, string>()
        {
            {Days.Monday, "1 День"},
            {Days.Tuesday, "2 День"},
            {Days.Wednesday, "3 День"},
            {Days.Thursday, "4 День"},
            {Days.Friday, "5 День"},
            {Days.Saturday, "6 День"},
            {Days.Sunday, "7 День"},
            {Days.Sunday, "ФИНАЛЬНЫЙ ДЕНЬ"}
        };
    private void Start()
    {
       
    }

    public void Init(Days currentDay)
    {
        _textField.text = Dayss[currentDay];
    }
    //public void Init(Days currentDay)
    //{
    //    _textField.text = currentDay.ToString();
    //}
}
