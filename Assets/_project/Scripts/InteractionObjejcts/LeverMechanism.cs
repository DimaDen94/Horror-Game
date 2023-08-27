using System;
using DG.Tweening;
using UnityEngine;

public class LeverMechanism : InteractionObject
{
    [SerializeField] private AudioSource _audioSourcePut;
    [SerializeField] private AudioSource _audioSourceSwitch;
    [SerializeField] private Lever _lever;
    private const int MovementDuration = 2;
    private bool _isActivated = false;
    private Vector3 _leverFinishRotation = new Vector3(0, -90, 90);

    public event Action MechanismActivated;

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is Lever)
        {
            PutInSlot((Lever)slot.Thing);
            slot.RemoveThing();
            return true;
        }
        else if (_lever != null && !_isActivated) {
            _lever.transform.DOLocalRotate(_leverFinishRotation, MovementDuration);
            _audioSourceSwitch.Play();
            MechanismActivated?.Invoke();
            _isActivated = true;
            return true;
        }
        return false;
    }

    private void PutInSlot(Lever lever)
    {
        lever.GetComponent<Collider>().enabled = false;
        lever.transform.parent = transform;
        lever.transform.DOLocalMove(lever.MechanismSlotPosition, MovementDuration);
        lever.transform.DOLocalRotate(lever.MechanismSlotRotation, MovementDuration).OnComplete(()=> {
            _audioSourcePut.Play();
        });
        _lever = lever;
    }
}