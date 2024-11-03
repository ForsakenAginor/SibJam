using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Desk_scr : MonoBehaviour
{
    public List<Class_List> Lists;
    //public Image List1;
    //public Image List2;
    //public Image List3;
    public GameObject Listprefab;
    public GameObject List1;
    public TextMeshProUGUI Count;
    public Canvas canvas;
    //public bool List1Active;
    //public bool List2Active;
    //public bool List3Active;
    public List<bool> ListActive;
    void Start()
    {
        Count.text = "0/3";
        gameObject.SetActive(false);
        //List1.GetComponent<Image>().color = Color.clear;
        //List2.GetComponent<Image>().color = Color.clear;
        //List3.GetComponent<Image>().color = Color.clear;
        ListActive.Add(false);
        ListActive.Add(false);
        ListActive.Add(false);
        //List1Active = false;
        //List2Active = false;
        //List3Active = false;
    }

    void Update()
    {

    }

    public void OpenDesk()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void NewList()
    {
        if (Lists.Count < 3)
        {
            Count.text = Lists.Count + 1 + "/3";
            Lists.Add(new Class_List() { Name = "Rats", Description = "cute" });
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            List1 = Instantiate(Listprefab,new Vector2(Input.mousePosition.x, Input.mousePosition.y) , Quaternion.identity);
            List1.transform.SetParent(canvas.transform, false);
        }

    }
}
