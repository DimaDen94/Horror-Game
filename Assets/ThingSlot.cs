using DG.Tweening;
using UnityEngine;

public class ThingSlot : MonoBehaviour
{
    private const int PutDuration = 1;

    private LiftedThing _thing;

    public void PutThing(LiftedThing thing)
    {
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