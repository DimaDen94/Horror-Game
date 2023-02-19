using UnityEngine;

public class LiftedThing : InteractionObject
{
    [SerializeField] private Vector3 _slotRotation;

    public Vector3 SlotRotation => _slotRotation;
}
