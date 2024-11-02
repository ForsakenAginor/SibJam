using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk_Lists : MonoBehaviour
{
    public List<class_list> Desk_lists_list;
    private IEventsInfoGetter A;
    
    void Start()
    {
       // Desk_lists_list.Add(new class_list() { Name = "1", Description = "2" });
    }
    public void init(IEventsInfoGetter a)
    {
        a = A;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
