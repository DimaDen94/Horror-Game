using System;

public class CollectionItem : InteractionObject
{
    public event Action ItemCollected;
    public override InteractionResponse TryUse(HeroSlot slot)
    {
        ItemCollected?.Invoke();
        Destroy(gameObject);
        return InteractionResponse.Activate;
    }
}
