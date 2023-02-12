using UnityEngine;

public class HeroSlot : MonoBehaviour
{
    private LiftedThing _thing;
    public void Put(LiftedThing thing)
    {
        _thing = thing;
        _thing.transform.parent = transform;
        _thing.transform.position = transform.position;
    }
}
