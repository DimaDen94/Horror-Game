using UnityEngine;

public interface IUIFactory
{
    GameObject Hud { get; }

    void CreateGameHud();
    MainMenuMediator CreateMainMenuHud();
}