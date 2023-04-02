using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class LeverMechanism : InteractionObject
{
    private const int MovementDuration = 2;
    private Lever _lever;
    private bool _isActivated = false;
    private Vector3 _leverFinishRotation = new Vector3(0, 0, 0);

    public UnityEvent MechanismActivated;

    public override void TryUse(HeroSlot slot)
    {
        if (slot.Thing is Lever)
        {
            PutInSlot((Lever)slot.Thing);
            slot.RemoveThing();
        }
        else if (_lever != null && !_isActivated) {
            _lever.transform.DOLocalRotate(_leverFinishRotation, MovementDuration);
            MechanismActivated?.Invoke();
            _isActivated = true;
        }
    }

    private void PutInSlot(Lever lever)
    {
        lever.GetComponent<Collider>().enabled = false;
        lever.transform.parent = transform;
        lever.transform.DOLocalMove(lever.MechanismSlotPosition, MovementDuration);
        lever.transform.DOLocalRotate(lever.MechanismSlotRotation, MovementDuration);
        _lever = lever;
    }
}
