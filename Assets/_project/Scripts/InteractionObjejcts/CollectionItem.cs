using System;

public class CollectionItem : InteractionObject
{
    public event Action ItemCollected;
    public override bool TryUse(HeroSlot slot)
    {
        ItemCollected?.Invoke();
        Destroy(gameObject);
        return false;
    }
}
