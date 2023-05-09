using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public virtual bool TryUse(HeroSlot slot) {
        return true;
    }
}