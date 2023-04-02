using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pot : InteractionObject
{
    [SerializeField]private Transform _upPoint;
    [SerializeField]private GameObject _dust;
    private const int MovementDuration = 1;
    private const int _foolCount = 3;
    private List<Mushroom> _mushrooms = new List<Mushroom>();

    public override void TryUse(HeroSlot slot)
    {
        if (slot.Thing is Mushroom)
        {
            PutInSlot((Mushroom)slot.Thing);
            slot.RemoveThing();
            if (_mushrooms.Count == _foolCount)
                _dust.gameObject.SetActive(true);
        }
        else if (_mushrooms.Count == _foolCount && slot.Thing is Bottle)
        {
            ChurnPotion((Bottle)slot.Thing, slot);
        }
    }

    private void ChurnPotion(Bottle bottle, HeroSlot slot)
    {
        slot.RemoveThing();
        Vector3 startPosition = bottle.transform.position;
        
        bottle.transform.DOMove(_upPoint.position, MovementDuration).OnComplete(
            () => bottle.transform.DOMove(transform.position, MovementDuration).OnComplete(
                () => {
                    slot.Put(bottle);
                    bottle.SetRedSkin();
                }));
    }

    private void PutInSlot(Mushroom mushroom)
    {
        mushroom.GetComponent<Collider>().enabled = false;
        mushroom.transform.parent = transform;
        mushroom.transform.DOMove(_upPoint.position, MovementDuration).OnComplete(() => mushroom.transform.DOMove(transform.position,MovementDuration));
        _mushrooms.Add(mushroom);
    }
}
