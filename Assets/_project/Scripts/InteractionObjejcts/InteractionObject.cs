using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    protected bool _canUse = true;
    [SerializeField] private Outline _outline;

    public bool CanUse => _canUse;

    public virtual InteractionResponse TryUse(HeroSlot slot) => InteractionResponse.Wrong;

    public void ShowOutline()
    {
        if (_outline == null)
            _outline = gameObject.AddComponent<Outline>();
        else if (!_outline.enabled)
            _outline.enabled = true;
    }

    public void HideOutline()
    {
        if (_outline != null && _outline.enabled)
            _outline.enabled = false;

    }
}

public enum InteractionResponse
{
    Used, Activate, Wrong
}