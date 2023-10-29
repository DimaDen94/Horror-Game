using UnityEngine;

public class UIFactory : IUIFactory
{
    private IAssetProvider _assetProvider;
    private ILocalizationService _localizationService;

    private Hud _hud;
    private BlackoutMediator _blackout;

    public UIFactory(IAssetProvider assetProvider, ILocalizationService localizationService) {
        _assetProvider = assetProvider;
        _localizationService = localizationService;
    }

    public GameObject Hud => _hud.gameObject;

    public BlackoutMediator Blackout => _blackout;

    public Hud CreateGameHud() => _hud = _assetProvider.Instantiate(AsserPath.HudPath).GetComponent<Hud>();

    public MainMenuMediator CreateMainMenuHud() => _assetProvider.Instantiate(AsserPath.MainMenuHudPath).GetComponent<MainMenuMediator>();

    public BlackoutMediator CreateBlackout() => _blackout = _assetProvider.Instantiate(AsserPath.BlackoutPath).GetComponent<BlackoutMediator>();

    public BlackoutMediator CreateFinishBlackout() => _blackout = _assetProvider.Instantiate(AsserPath.FinishBlackoutPath).GetComponent<BlackoutMediator>();

    public SettingMenuMediator CreateSettingMenuHud() => _assetProvider.Instantiate(AsserPath.SettingMenuHudPath).GetComponent<SettingMenuMediator>();

    public PauseMenuMediator CreatePauseMenu() => _assetProvider.Instantiate(AsserPath.PauseMenuHudPath).GetComponent<PauseMenuMediator>();

    public HintMenuMediator CreateHintMenu() => _assetProvider.Instantiate(AsserPath.HintMenuHudPath).GetComponent<HintMenuMediator>();

    public StoryBlackoutMediator CreateStoryBlackout()
    {
        _blackout = _assetProvider.Instantiate(AsserPath.StoryBlackoutPath).GetComponent<BlackoutMediator>();
        return _blackout.GetComponent<StoryBlackoutMediator>();
    }
}
