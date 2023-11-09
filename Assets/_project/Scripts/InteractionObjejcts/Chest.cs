using System;
using DG.Tweening;
using UnityEngine;

public class Chest : InteractionObject
{
    public event Action ChestOpened;

    [SerializeField] private Transform _chestCover;
    [SerializeField] private GameObject _loot;
    [SerializeField] private AudioSource _audioSource;
    private const int OpenDuration = 2;

    private bool _isOpen = false;

    private IToastService _toastService;

    public void Construct(IToastService toastService) => _toastService = toastService;

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is ChestKey && !_isOpen)
        {
            OpenChest();
            _audioSource.Play();
            _isOpen = true;
            ChestOpened?.Invoke();
            return true;
        }
        if(!_isOpen)
            _toastService.ShowToast(TranslatableKey.NeedTheRightKey);

        return false;
    }

    private void OpenChest()
    {
        _chestCover.DOLocalRotate(Vector3.zero, OpenDuration);
        _loot.SetActive(true);
    }
}
