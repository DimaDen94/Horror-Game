using DG.Tweening;
using UnityEngine;

public class Hammer : LiftedThing
{
    private const float AnimateDuration = 0.25f;

    public override void Animate()
    {
        _canUse = false;
        var rotate = new Vector3(40, 160, 86);
        transform.DOLocalRotate(rotate, AnimateDuration).SetLoops(2, LoopType.Yoyo).OnComplete(()=> {
            _canUse = true;
        });
    }
}
