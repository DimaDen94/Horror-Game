using System;

public interface IIAPService
{
    bool Isinitialized { get; }

    event Action Initialized;
    event Action FullAccessCompleted;

    void Initialize();
    void StartPurchase(string productId);
    bool HasPurchase(PurchaseItemType fullAccess);
}