using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string hudPath, Vector3 position, Quaternion rotation) => Object.Instantiate(Resources.Load<GameObject>(hudPath), position, rotation);

    public GameObject Instantiate(string hudPath) => Object.Instantiate(Resources.Load<GameObject>(hudPath));


    public GameObject Instantiate(string path, Transform transform) => Object.Instantiate(Resources.Load<GameObject>(path), transform);

    public Type LoadScriptableObject<Type>(string path) where Type : ScriptableObject => Resources.Load<Type>(path);
}