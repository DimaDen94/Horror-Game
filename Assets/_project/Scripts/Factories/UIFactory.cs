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

    public FinalBlackoutMediator CreateFinishBlackout()
    {
        _blackout = _assetProvider.Instantiate(AsserPath.FinalBlackoutPath).GetComponent<BlackoutMediator>();
        return _blackout.GetComponent<FinalBlackoutMediator>();
    }

    public SettingMenuMediator CreateSettingMenuHud() => _assetProvider.Instantiate(AsserPath.SettingMenuHudPath).GetComponent<SettingMenuMediator>();

    public PauseMenuMediator CreatePauseMenu() => _assetProvider.Instantiate(AsserPath.PauseMenuHudPath).GetComponent<PauseMenuMediator>();

    public HintMenuMediator CreateHintMenu() => _assetProvider.Instantiate(AsserPath.HintMenuHudPath).GetComponent<HintMenuMediator>();

    public StoryBlackoutMediator CreateStoryBlackout()
    {
        _blackout = _assetProvider.Instantiate(AsserPath.StoryBlackoutPath).GetComponent<BlackoutMediator>();
        return _blackout.GetComponent<StoryBlackoutMediator>();
    }

    public ToastView CreateToast() => _assetProvider.Instantiate(AsserPath.ToastPath).GetComponent<ToastView>();

    public MemoryMenu CreateMemoryMenu() => _assetProvider.Instantiate(AsserPath.MemoryMenuPath).GetComponent<MemoryMenu>();

    public LanguageSelectionMediator CreateLanguageMenu() => _assetProvider.Instantiate(AsserPath.LanguageMenuPath).GetComponent<LanguageSelectionMediator>();

    public LanguageItemView CreateLanguageItemView(Transform parent) => _assetProvider.Instantiate(AsserPath.LanguageItemPath, parent).GetComponent<LanguageItemView>();
}
