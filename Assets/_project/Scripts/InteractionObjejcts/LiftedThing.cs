using System;
using DG.Tweening;
using UnityEngine;

public class LiftedThing : InteractionObject
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _slotRotation;

    public Vector3 SlotRotation => _slotRotation;

    public void DisablePhysics() {
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.Sleep();
    }

    public void EnablePhysics()
    {
        _collider.isTrigger = false;
        _rigidbody.useGravity = true;
        _rigidbody.WakeUp();
    }

   
}
