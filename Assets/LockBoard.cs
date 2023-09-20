using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBoard : InteractionObject
{
    private const int HitForce = 100;
    [SerializeField] private Rigidbody _rigidbody;

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is Hammer) 
            Hit();
        

        return false;
    }

    public void Hit()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.right * HitForce);
    }
}
