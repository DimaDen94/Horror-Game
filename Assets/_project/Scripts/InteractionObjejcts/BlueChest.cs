using DG.Tweening;
using UnityEngine;

public class BlueChest : InteractionObject
{
    [SerializeField] private Transform _chestCover;
    [SerializeField] private GameObject _loot;
    [SerializeField] private AudioSource _audioSource;
    private const int OpenDuration = 2;

    private bool _isOpen = false;

    private IToastService _toastService;


    public void Construct(IToastService toastService)
    {
        _toastService = toastService;
    }

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is BlueChestKye && !_isOpen)
        {
            OpenChest();
            _audioSource.Play();
            _isOpen = true;
            return true;
        }
        _toastService.ShowToast(TranslatableKey.NeedTheRightKey);
        return false;
    }

    private void OpenChest()
    {
        _chestCover.DOLocalRotate(Vector3.zero, OpenDuration);
        _loot.SetActive(true);
    }
}
