using DG.Tweening;
using UnityEngine;

public class HeroSlot : MonoBehaviour
{
    private const float RotateDuration = 0.25f;
    private const float _moveSpeed = 10;

    private LiftedThing _thing;

    public LiftedThing Thing => _thing;
    public bool IsFull => _thing != null;

    public void Put(LiftedThing thing)
    {
        thing.transform.parent = transform;
        _thing = thing;
        _thing.transform.DOLocalRotate(_thing.SlotRotation, RotateDuration);
        _thing.DisablePhysics();
    }
    public void Drop()
    {
        if (_thing == null)
            return;
        _thing.transform.parent = null;
        _thing.EnablePhysics();
        _thing = null;

    }

    private void Update()
    {
        if (_thing == null)
            return;

        if (Vector3.Distance(_thing.transform.position, transform.position) > Constants.Epsilon)
            _thing.transform.position = Vector3.MoveTowards(_thing.transform.position, transform.position, _moveSpeed * Time.deltaTime);

    }
}
