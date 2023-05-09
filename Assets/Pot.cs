using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pot : InteractionObject
{
    [SerializeField] private AudioSource _audioSourcePlop;
    [SerializeField] private AudioSource _audioSourceChemistry;
    [SerializeField] private Transform _upPoint;
    [SerializeField] private GameObject _dust;

    private const int MovementDuration = 1;
    private const int FoolCount = 3;

    private List<Mushroom> _mushrooms = new List<Mushroom>();

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is Mushroom)
        {
            PutInSlot((Mushroom)slot.Thing);
            slot.RemoveThing();
            if (_mushrooms.Count == FoolCount)
            {
                _dust.gameObject.SetActive(true);
                _audioSourceChemistry.Play();
            }
            return true;
        }
        else if (_mushrooms.Count == FoolCount && slot.Thing is Bottle)
        {
            ChurnPotion((Bottle)slot.Thing, slot);
            return true;
        }
        return false;
    }

    private void ChurnPotion(Bottle bottle, HeroSlot slot)
    {
        slot.RemoveThing();
        Vector3 startPosition = bottle.transform.position;
        
        bottle.transform.DOMove(_upPoint.position, MovementDuration).OnComplete(() =>
            {
                _audioSourcePlop.Play();
                bottle.transform.DOMove(transform.position, MovementDuration).OnComplete(() =>
                  {
                      slot.Put(bottle);
                      bottle.SetRedSkin();
                  });
            });
    }

    private void PutInSlot(Mushroom mushroom)
    {
        mushroom.GetComponent<Collider>().enabled = false;
        mushroom.transform.parent = transform;
        mushroom.transform.DOMove(_upPoint.position, MovementDuration).OnComplete(() => {
                _audioSourcePlop.Play();
                mushroom.transform.DOMove(transform.position, MovementDuration);
            });
        _mushrooms.Add(mushroom);
    }
}
