using DG.Tweening;
using UnityEngine;

public class RedChest : InteractionObject
{
    [SerializeField] private Transform _chestCover;
    [SerializeField] private GameObject _loot;
    [SerializeField] private AudioSource _audioSource;
    private const int OpenDuration = 2;

    private bool _isOpen = false;

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is RedChestKey && !_isOpen)
        {
            OpenChest();
            _audioSource.Play();
            _isOpen = true;
            return true;
        }
        return false;
    }

    private void OpenChest()
    {
        _chestCover.DOLocalRotate(Vector3.zero, OpenDuration);
        _loot.SetActive(true);
    }
}
