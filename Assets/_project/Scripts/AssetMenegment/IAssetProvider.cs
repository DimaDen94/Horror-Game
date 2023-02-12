using UnityEngine;

public interface IAssetProvider
{
    GameObject Instantiate(string hudPath);
}