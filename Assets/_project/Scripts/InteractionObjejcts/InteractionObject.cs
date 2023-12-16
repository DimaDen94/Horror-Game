using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    protected bool _canUse = true;

    public bool CanUse => _canUse;

    public virtual InteractionResponse TryUse(HeroSlot slot) {
        return InteractionResponse.Wrong;
    }
}

public enum InteractionResponse
{
    Used, Activate, Wrong
}