using UnityEngine;

public interface IUIFactory
{
    GameObject Hud { get; }
    BlackoutMediator Blackout { get; }

    BlackoutMediator CreateBlackout();
    FinalBlackoutMediator CreateFinishBlackout();
    Hud CreateGameHud();
    MainMenuMediator CreateMainMenuHud();
    SettingMenuMediator CreateSettingMenuHud();
    PauseMenuMediator CreatePauseMenu();
    StoryBlackoutMediator CreateStoryBlackout();
    HintMenuMediator CreateHintMenu();
    ToastView CreateToast();
}