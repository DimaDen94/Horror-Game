using UnityEngine;

public class Lever : LiftedThing
{
    [SerializeField] private Vector3 _mechanismSlotRotation;
    [SerializeField] private Vector3 _mechanismSlotPosition;

    public Vector3 MechanismSlotRotation => _mechanismSlotRotation;

    public Vector3 MechanismSlotPosition => _mechanismSlotPosition;
}
