using UnityEngine;

public class UIFactory : IUIFactory
{
    private IAssetProvider _assetProvider;

    private GameObject _hud;

    public UIFactory(IAssetProvider assetProvider) {
        _assetProvider = assetProvider;
    }

    public GameObject Hud => _hud;

    public void CreateHud() {
        _hud = _assetProvider.Instantiate(AsserPath.HudPath);
    }
}
