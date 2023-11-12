using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IAP/ProductList", order = 51)]
public class IAPProductConfigs : ScriptableObject
{
    public List<ProductConfig> products;
}
