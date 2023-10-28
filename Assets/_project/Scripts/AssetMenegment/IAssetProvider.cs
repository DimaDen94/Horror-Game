using UnityEngine;

public interface IAssetProvider
{
    GameObject Instantiate(string hudPath, Vector3 heroStartPosition, Quaternion quaternion);
    GameObject Instantiate(string hudPath);
    Type LoadScriptableObject<Type>(string path) where Type : ScriptableObject;
}