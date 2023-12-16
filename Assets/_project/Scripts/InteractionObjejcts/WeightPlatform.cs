using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WeightPlatform : InteractionObject
{
    private const int MoveDuration = 1;

    [SerializeField] private float _maxYOffset;
    
    [SerializeField] private List<ThingSlot> _slots;
    [SerializeField] private AudioSource _moveAudio;

    public event Action WeightAdded;

    public override InteractionResponse TryUse(HeroSlot slot)
    {
        ThingSlot emptySlot = _slots.Find(slot => slot.IsEmpty);
        if (emptySlot != null && slot.Thing is WeightThing)
        {
            emptySlot.PutThing(slot.Thing);
            slot.RemoveThing();
            MovePlatform();
            WeightAdded.Invoke();

            return InteractionResponse.Used;
        }
        return InteractionResponse.Wrong;
    }

    public bool IsFull() {
        if (_slots.Find(slot => slot.IsEmpty) == null)
            return true;
        else
            return false;
    }

    private void MovePlatform()
    {
        float slotCount = _slots.Count;
        float slotFullCount = _slots.FindAll(slot => !slot.IsEmpty).Count;
        var currentOfset = _maxYOffset * slotFullCount / slotCount;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1);
        sequence.Append(transform.DOMoveY(currentOfset, MoveDuration));
        _moveAudio.Play();
    }
}
