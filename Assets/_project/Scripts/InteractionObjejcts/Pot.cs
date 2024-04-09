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
    private IToastService _toastService;
    private IAnalyticService _analyticService;

    public void Construct(IToastService toastService, IAnalyticService analyticService)
    {
        _toastService = toastService;
        _analyticService = analyticService;
    }

    public override InteractionResponse TryUse(HeroSlot slot)
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
            _analyticService.MashroomInPot(_mushrooms.Count, slot.Thing.name);
            return InteractionResponse.Used;
        }
        else if (_mushrooms.Count == FoolCount && slot.Thing is Bottle && !((Bottle)slot.Thing).IsRed())
        {
            ChurnPotion((Bottle)slot.Thing, slot);
            _analyticService.BottleInPot();
            return InteractionResponse.Used;
        }
        else if (slot.Thing is Bottle && ((Bottle)slot.Thing).IsRed()) {
            _toastService.ShowToast(TranslatableKey.ThePotionIsReady);
        }
        else if (slot.Thing is Bottle)
        {
            _toastService.ShowToast(TranslatableKey.ThePotionIsNotReady);
        }

        return InteractionResponse.Wrong;
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
