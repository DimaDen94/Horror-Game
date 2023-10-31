using System;
using UnityEngine;

public class LiftedThing : InteractionObject
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _slotRotation;
    private GameObject _light;

    public Vector3 SlotRotation => _slotRotation;

    public event Action Lifted;

    public void DisablePhysics() {
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.Sleep();

        Lifted?.Invoke();

        _light?.SetActive(false);
    }

    public void EnablePhysics()
    {
        _collider.isTrigger = false;
        _rigidbody.useGravity = true;
        _rigidbody.WakeUp();

        _light?.SetActive(true);
    }

    public void SetLight(GameObject light)
    {
        _light = light;
    }
}
