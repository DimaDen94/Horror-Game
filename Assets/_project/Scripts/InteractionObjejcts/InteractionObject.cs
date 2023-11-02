using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    protected bool _canUse = true;

    public bool CanUse => _canUse;
    public virtual bool TryUse(HeroSlot slot) {
        return false;
    }
}