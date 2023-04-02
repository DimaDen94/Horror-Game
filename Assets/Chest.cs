using DG.Tweening;
using UnityEngine;

public class Chest : InteractionObject
{
    [SerializeField] private Transform _chestCover;
    [SerializeField] private GameObject _loot;
    private const int OpenDuration = 2;

    public override void TryUse( HeroSlot slot)
    {
        if (slot.Thing is ChestKey)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        _chestCover.DOLocalRotate(Vector3.zero, OpenDuration);
        _loot.SetActive(true);
    }
}
