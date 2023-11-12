using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;

public partial class IAPProvider : IDetailedStoreListener
{
    public Dictionary<string, Product> Products;

    private IAPService _iapService;
    private IStoreController _controller;
    private IExtensionProvider _extensions;
    private IAPProductConfigs _configs;

    private const string IAPConfigsPath = "IAP/IAP Product Configs";

    public event Action Initialized;

    public IAPProductConfigs Configs => _configs;


    public void Initialize(IAPService iapService)
    {
        Load();
        _iapService = iapService;
        Products = new Dictionary<string, Product>();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach (ProductConfig config in _configs.products)
            AddProduct(builder, config);

        UnityPurchasing.Initialize(this, builder);
    }



    public Product GetProductByType(PurchaseItemType type) => Products[GetProductId(type)];

    public void StartPurchase(string productId) =>
        _controller.InitiatePurchase(productId);

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _controller = controller;
        _extensions = extensions;
        Initialized?.Invoke();

        Products = _controller.products.all.ToDictionary(product => product.definition.id, product => product);

        Debug.Log("Unity Purchasing Initialized");
    }

    public bool Isinitialized =>
        _controller != null && _extensions != null;



    public bool ReceiptCheck(PurchaseItemType itemType)
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Product[] products = _controller.products.all;
            try
            {
                CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
                for (int i = 0; i < products.Length; i++)
                {
                    if (products[i].receipt == null)
                        continue;
                    try
                    {
                        IPurchaseReceipt[] result = validator.Validate(products[i].receipt);
                        if (result[0].productID.Equals(GetProductId(itemType)))
                            return true;
                    }
                    catch (IAPSecurityException e)
                    {
                        Debug.LogWarning("exception " + e);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning("Unexpected exception " + e);
                    }
                }
            }
            catch
            {
                Debug.Log("CrossPlatformValidator ERROR");
            }
        }
        return false;
    }

    public void OnInitializeFailed(InitializationFailureReason error) =>
       Debug.Log($"Unity Purchasing Initialized{error}");

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
        Debug.Log($"Product {product.definition.id} Purchase Failed, PurchaseFailureReason - {failureReason}, transaction id = {product.transactionID}");

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.Log($"Unity Purchasing ProcessPurchase success {purchaseEvent.purchasedProduct.definition.id}");
        return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
    }

    private string GetProductId(PurchaseItemType type)
    {
#if UNITY_ANDROID
        return Configs.products.Find(prod => prod.ItemType == type).AndroidId;
#elif UNITY_IPHONE
        return Configs.products.Find(prod => prod.ItemType == type).iosId;
#endif
    }

    private void AddProduct(ConfigurationBuilder builder, ProductConfig config)
    {
#if UNITY_ANDROID
        builder.AddProduct(config.AndroidId, config.Type);
#elif UNITY_IPHONE
        builder.AddProduct(config.iosId, config.Type);
#endif
    }

    private void Load() => _configs = Resources.Load<IAPProductConfigs>(IAPConfigsPath);


    public void RestorePurchases()
    {
        if (!Isinitialized)
            return;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            var apple = _extensions.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((state,message) => {
                _iapService.Restore();
            });
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log(message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log(failureDescription.message);
    }
}
