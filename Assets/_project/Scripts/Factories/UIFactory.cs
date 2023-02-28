using UnityEngine;

public class UIFactory : IUIFactory
{
    private IAssetProvider _assetProvider;

    private GameObject _hud;

    public UIFactory(IAssetProvider assetProvider) {
        _assetProvider = assetProvider;
    }

    public GameObject Hud => _hud;

    public void CreateGameHud() =>
        _hud = _assetProvider.Instantiate(AsserPath.HudPath);

    public MainMenuMediator CreateMainMenuHud() =>
        _assetProvider.Instantiate(AsserPath.MainMenuHudPath).GetComponent<MainMenuMediator>();
}
