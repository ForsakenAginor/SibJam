using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_add_button : MonoBehaviour
{
    [SerializeField] private GameObject Desk;
    [SerializeField] private GameObject List;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Instantiate(List, new Vector2(Desk.transform.position.x, Desk.transform.position.y), Quaternion.identity);
    }
}
