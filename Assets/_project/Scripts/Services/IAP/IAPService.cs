using System;
using UnityEngine.Purchasing;
using Zenject;

public class IAPService : IIAPService
{
    private readonly IAPProvider _provider;
    private readonly IProgressService _progressService;
    public bool Isinitialized => _provider.Isinitialized;
    public event Action Initialized;
    public event Action FullAccessCompleted;

    public IAPService(IAPProvider provider, IProgressService progressService) {
        _provider = provider;
        _progressService = progressService;
    }

    public void Initialize()
    {
        _provider.Initialize(this);
        _provider.Initialized += () => Initialized?.Invoke();
    }

    public void StartPurchase(PurchaseItemType purchase) =>
        StartPurchase(_provider.GetProductByType(purchase).definition.id);

    public void StartPurchase(string productId) =>
        _provider.StartPurchase(productId);

    public PurchaseProcessingResult ProcessPurchase(Product purchaseProduct)
    {
        ProductConfig profuctConfig = GetProductConfig(purchaseProduct);
        switch (profuctConfig.ItemType)
        {
            case PurchaseItemType.DisableAdvertising:
                _progressService.PurchaseProduct(PurchaseItemType.DisableAdvertising.ToString());
                FullAccessCompleted?.Invoke();
                break;
        }
        return PurchaseProcessingResult.Complete;
    }

    public bool HasPurchase(PurchaseItemType fullAccess)
    {
       return _provider.ReceiptCheck(fullAccess);
    }

    private ProductConfig GetProductConfig(Product purchaseProduct)
    {
#if UNITY_ANDROID
        return _provider.Configs.products.Find(prod => prod.AndroidId.Equals(purchaseProduct.definition.id));
#elif UNITY_IPHONE
        return _provider.Configs.products.Find(prod => prod.iosId.Equals(purchaseProduct.definition.id));
#endif
    }

    public void Restore()
    {
        if (HasPurchase(PurchaseItemType.DisableAdvertising)){
            _progressService.PurchaseProduct(PurchaseItemType.DisableAdvertising.ToString());
            FullAccessCompleted?.Invoke();
        }

    }
}