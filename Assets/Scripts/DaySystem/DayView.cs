using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;
    public GameObject text;
    public Animator _animator;


    
    Dictionary<Days, string> Dayss = new Dictionary<Days, string>()
        {
            {Days.Monday, "1 День"},
            {Days.Tuesday, "2 День"},
            {Days.Wednesday, "3 День"},
            {Days.Thursday, "4 День"},
            {Days.Friday, "5 День"},
            {Days.Saturday, "6 День"},
            {Days.Sunday, "7 День"},
            {Days.Final, "ФИНАЛЬНЫЙ ДЕНЬ"}
        };

    public void Awake()
    {
        _animator = text.GetComponent<Animator>();
    }


    private void Start()
    {
    }

    public void Init(Days currentDay)
    {
        _textField.text = Dayss[currentDay];
        _animator.SetBool("next", true);
    }
    //public void Init(Days currentDay)
    //{
    //    _textField.text = currentDay.ToString();
    //}
}
