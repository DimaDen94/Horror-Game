using System;
using UnityEngine;

public class LiftedThing : InteractionObject
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _slotRotation;

    public Vector3 SlotRotation => _slotRotation;

    public event Action Lifted;

    public void DisablePhysics() {
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.Sleep();

        Lifted?.Invoke();
    }

    public void EnablePhysics()
    {
        _collider.isTrigger = false;
        _rigidbody.useGravity = true;
        _rigidbody.WakeUp();
    }
}
