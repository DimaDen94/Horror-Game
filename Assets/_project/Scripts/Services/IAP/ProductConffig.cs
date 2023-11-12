using System;
using UnityEngine.Purchasing;


[Serializable]
public class ProductConfig
{
    public string AndroidId;
    public ProductType Type;
    public int MaxPurchaseCount;
    public PurchaseItemType ItemType;
}

public enum PurchaseItemType
{
    DisableAdvertising
}