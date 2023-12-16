using System;
using UnityEngine;

public class ExitDoor : InteractionObject
{
    [SerializeField]private AudioSource _audioSource;

    public event Action ExitDoorOpened;

    private IToastService _toastService;


    public void Construct(IToastService toastService)
    {
        _toastService = toastService;
    }

    public override InteractionResponse TryUse(HeroSlot slot)
    {
        if (slot.Thing is ExitKey)
        {
            _audioSource.Play();

            NextLevel();
            return InteractionResponse.Used;

        }
        _toastService.ShowToast(TranslatableKey.NeedTheRightKey);
        return InteractionResponse.Wrong;
    }

    private void NextLevel()
    {
        ExitDoorOpened?.Invoke();
    }
}
