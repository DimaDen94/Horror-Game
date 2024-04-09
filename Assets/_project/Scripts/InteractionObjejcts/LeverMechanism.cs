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
    private const float MovementDuration = 1.5f;
    private bool _isActivated = false;
    private IToastService _toastService;

    public event Action MechanismActivated;
    public event Action MechanismFixed;
    
    public bool IsActivated => _isActivated;


    public void Construct(IToastService toastService)
    {
        _toastService = toastService;
    }


    public override InteractionResponse TryUse(HeroSlot slot)
    {
        if (slot.Thing is Lever)
        {
            PutInSlot((Lever)slot.Thing);
            slot.RemoveThing();
            MechanismFixed?.Invoke();
            return InteractionResponse.Used;
        }
        else if (_lever != null && !IsActivated && !_lock)
        {
            _lock = true;
            _lever.transform.DOLocalRotate(_leverFinishRotation, MovementDuration).OnComplete(() => { _lock = false; });

            _audioSourceSwitch.Play();
            _isActivated = true;
            MechanismActivated?.Invoke();
            return InteractionResponse.Activate;
        }
        else if (_lever != null && IsActivated && !_lock)
        {
            _lock = true;
            _lever.transform.DOLocalRotate(_leverStartRotation, MovementDuration).OnComplete(() => { _lock = false; }); ;

            _audioSourceSwitch.Play();
            _isActivated = false;
            MechanismActivated?.Invoke();
            return InteractionResponse.Activate;
        }
        else if (_lever == null)
        {
            _toastService.ShowToast(TranslatableKey.ThisMechanismIsBroken);
        }

        return InteractionResponse.Wrong;
    }

    private void PutInSlot(Lever lever)
    {
        lever.GetComponent<Collider>().enabled = false;
        lever.transform.parent = _leverContainer;
        lever.MeshUp();
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
