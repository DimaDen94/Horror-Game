using UnityEngine;

public class UIFactory : IUIFactory
{
    private IAssetProvider _assetProvider;

    private Hud _hud;
    private BlackoutMediator _blackout;

    public UIFactory(IAssetProvider assetProvider) {
        _assetProvider = assetProvider;
    }

    public GameObject Hud => _hud.gameObject;

    public BlackoutMediator Blackout => _blackout;

    public Hud CreateGameHud() => _hud = _assetProvider.Instantiate(AsserPath.HudPath).GetComponent<Hud>();

    public MainMenuMediator CreateMainMenuHud() => _assetProvider.Instantiate(AsserPath.MainMenuHudPath).GetComponent<MainMenuMediator>();

    public BlackoutMediator CreateBlackout() => _blackout = _assetProvider.Instantiate(AsserPath.BlackoutPath).GetComponent<BlackoutMediator>();

}
