using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string hudPath, Vector3 position, Quaternion rotation)
    {
        GameObject prefab =  Resources.Load<GameObject>(hudPath);
        return Object.Instantiate(prefab, position,rotation);
    }
    public GameObject Instantiate(string hudPath)
    {
        GameObject prefab = Resources.Load<GameObject>(hudPath);
        return Object.Instantiate(prefab);
    }
}