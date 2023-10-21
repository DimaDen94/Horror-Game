using UnityEngine;

public interface IUIFactory
{
    GameObject Hud { get; }
    BlackoutMediator Blackout { get; }

    BlackoutMediator CreateBlackout();
    BlackoutMediator CreateFinishBlackout();
    Hud CreateGameHud();
    MainMenuMediator CreateMainMenuHud();
    SettingMenuMediator CreateSettingMenuHud();
    PauseMenuMediator CreatePauseMenu();
}