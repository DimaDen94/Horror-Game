using UnityEngine;

public interface IUIFactory
{
    GameObject Hud { get; }

    void CreateHud();
}