using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class LeverMechanism : InteractionObject
{
    [SerializeField] private AudioSource _audioSourcePut;
    [SerializeField] private AudioSource _audioSourceSwitch;
    [SerializeField] private Transform _leverContainer;
    [SerializeField] private Lever _lever;

    [SerializeField] private Vector3 _leverFinishRotation = new Vector3(0, -90, 0);
    [SerializeField] private Vector3 _leverStartRotation = new Vector3(0, 0, 0);

    private bool _lock = false;
    private const int MovementDuration = 3;
    private bool _isActivated = false;


    public event Action MechanismActivated;
    
    public bool IsActivated => _isActivated;

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is Lever)
        {
            PutInSlot((Lever)slot.Thing);
            slot.RemoveThing();
            return true;
        }else if (_lever != null && !IsActivated && !_lock) {
            _lock = true;
            _lever.transform.DOLocalRotate(_leverFinishRotation, MovementDuration).OnComplete(()=> { _lock = false; });

            _audioSourceSwitch.Play();
            _isActivated = true;
            MechanismActivated?.Invoke();
            return true;
        }else if (_lever != null && IsActivated && !_lock)
        {
            _lock = true;
            _lever.transform.DOLocalRotate(_leverStartRotation, MovementDuration).OnComplete(() => { _lock = false; }); ;

            _audioSourceSwitch.Play();
            _isActivated = false;
            MechanismActivated?.Invoke();
            return true;
        }
        return false;
    }

    private void PutInSlot(Lever lever)
    {
        lever.GetComponent<Collider>().enabled = false;
        lever.transform.parent = _leverContainer;
        lever.transform.DOMove(_leverContainer.position, MovementDuration);
        lever.transform.DOLocalRotate(_leverStartRotation, MovementDuration).OnComplete(()=> {
            _audioSourcePut.Play();
        });
        _lever = lever;
    }

    [Button]
    private void MechanismActivateTest() {
        MechanismActivated?.Invoke();
    }

}
