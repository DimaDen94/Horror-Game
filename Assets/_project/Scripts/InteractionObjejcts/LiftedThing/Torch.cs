using DG.Tweening;
using UnityEngine;

public class Torch : WeightThing
{
    [SerializeField] private GameObject _fire;
    private const float AnimateDuration = 0.25f;

    private bool _isBurning = false;

    public bool IsBurning => _isBurning;

    public void Light() {
        _isBurning = true;
        _fire.SetActive(true);
        EnablePhysics();
        GetComponent<Collider>().enabled = true;
    }


    public override void Animate()
    {
        _canUse = false;
        var rotate = new Vector3(48, 16, 25);
        transform.DOLocalRotate(rotate, AnimateDuration).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            _canUse = true;
        });
    }
}