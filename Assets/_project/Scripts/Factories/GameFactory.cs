using UnityEngine;

public class GameFactory : IGameFactory
{
    private IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider) => _assetProvider = assetProvider;

    public Hero CreateHero(Vector3 heroStartPosition, Quaternion quaternion) => _assetProvider.Instantiate(AsserPath.HeroPath, heroStartPosition,quaternion).GetComponent<Hero>();

    public GameObject CreateLight(Transform transform) => _assetProvider.Instantiate(AsserPath.LightPath, transform);
}
