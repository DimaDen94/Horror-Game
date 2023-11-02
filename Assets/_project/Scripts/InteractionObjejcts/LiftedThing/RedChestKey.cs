using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RedChestKey : LiftedThing
{
    private const float AnimateDuration = 0.5f;

    public override void Animate()
    {
        var rotate = new Vector3(160, 100, -55);
        transform.DOLocalRotate(rotate, AnimateDuration).SetLoops(2, LoopType.Yoyo);
    }
}

