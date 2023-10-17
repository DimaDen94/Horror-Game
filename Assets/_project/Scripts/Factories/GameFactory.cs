public class GameFactory : IGameFactory
{
    private IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public Hero CreateHero() => _assetProvider.Instantiate(AsserPath.HeroPath).GetComponent<Hero>();
}
