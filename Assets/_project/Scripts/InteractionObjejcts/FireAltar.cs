using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAltar : InteractionObject
{
    [SerializeField] private ThingSlot _slot;
    [SerializeField] private ParticleSystem _fire;

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing != null)
            return false;

        _fire.Play();

        if (_slot.Thing is Torch)
        {
            _slot.GetComponent<Collider>().enabled = false;
            ((Torch)_slot.Thing).Light();
        }
        return false;
    }
}
