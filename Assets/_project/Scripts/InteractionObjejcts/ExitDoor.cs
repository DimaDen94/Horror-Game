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

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is ExitKey)
        {
            _audioSource.Play();

            NextLevel();
            return true;

        }
        _toastService.ShowToast(TranslatableKey.NeedTheRightKey);
        return false;
    }

    private void NextLevel()
    {
        ExitDoorOpened?.Invoke();
    }
}
