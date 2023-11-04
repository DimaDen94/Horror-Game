using DG.Tweening;
using UnityEngine;

public class ChestKey : LiftedThing
{
    private const float AnimateDuration = 0.5f;

    public override void Animate()
    {
        var rotate = new Vector3(183.55f, -84.34799f, 65.628f);
        transform.DOLocalRotate(rotate, AnimateDuration).SetLoops(2, LoopType.Yoyo);
    }
}

