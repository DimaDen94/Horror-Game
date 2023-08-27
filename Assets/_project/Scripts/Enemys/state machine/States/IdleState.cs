using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Animator _animator;

 
    private void OnEnable()
    {
        _animator = GetComponent<Enemy>().Animator;
        _animator.SetTrigger("Idle");
    }
    private void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            _animator.SetTrigger("Idle");
        
    }
}
