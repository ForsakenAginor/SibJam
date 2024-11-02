using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Screen_start : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Anim()
    {
        _animator.SetBool("Start", true);
    }
}
