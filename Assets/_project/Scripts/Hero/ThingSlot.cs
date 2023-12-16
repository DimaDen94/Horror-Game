using DG.Tweening;
using UnityEngine;

public class ThingSlot : InteractionObject
{
    private const int PutDuration = 1;

    private LiftedThing _thing;

    public LiftedThing Thing  => _thing;

    public override InteractionResponse TryUse(HeroSlot slot)
    {
        if (!(slot.Thing is WeightThing))
            return InteractionResponse.Wrong;
        else
        {
            PutThing(slot.Thing);
            slot.RemoveThing();
            return InteractionResponse.Used;
        }

    }

    public void PutThing(LiftedThing thing)
    {
        if (!(thing is WeightThing))
            return;

        _thing = thing;
        _thing.GetComponent<Collider>().enabled = false;
        MoveToSlot();
    }

    private void MoveToSlot()
    {
        _thing.transform.parent = transform;
        _thing.transform.DOMove(transform.position, PutDuration);
        _thing.transform.DORotate(Vector3.zero, PutDuration);
    }

    public bool IsEmpty => _thing == null;

}