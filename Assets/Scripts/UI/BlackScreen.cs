using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public GameObject Black_screen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }



    
}
