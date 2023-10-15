using System;
using UnityEngine;
using Zenject;

public class ExitDoor : InteractionObject
{
    [SerializeField]private AudioSource _audioSource;
    private IUIFactory _uiFactory;

    public event Action ExitDoorOpened;

    [Inject]
    private void Constract(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is ExitKey)
        {
            _audioSource.Play();

            Invoke("NextLevel", 0.5f);
            return true;

        }
        return false;
    }

    private void NextLevel()
    {
        ExitDoorOpened?.Invoke();
    }
}
