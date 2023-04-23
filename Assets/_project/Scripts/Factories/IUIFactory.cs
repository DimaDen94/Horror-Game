using UnityEngine;

public interface IUIFactory
{
    GameObject Hud { get; }
    BlackoutMediator Blackout { get; }

    BlackoutMediator CreateBlackout();
    Hud CreateGameHud();
    MainMenuMediator CreateMainMenuHud();
}