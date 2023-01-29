using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        RegisterInputService();
    }

    private void RegisterInputService()
    {
        if (Application.isEditor)
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
        else
            Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
    }
}
