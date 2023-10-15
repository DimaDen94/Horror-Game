using System;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private GameBootstrapper _bootstrapperPrefab;
    [SerializeField] private SceenLoader _sceenLoader;

    public override void InstallBindings()
    {
        BindPlayerPrefsService();
        BindJsonConvertor();
        ProgressService();
        BindSceenLoader();
        BindAudioServiceLoader();
        BindInputService();
        BindStateMachine();
        BindAssetProvider();
        BindUIFactory();
        BindGameBootstrapper();
    }

    private void BindPlayerPrefsService() => Container.Bind<IPlayerPrefsService>().To<PlayerPrefsService>().AsSingle();

    private void BindJsonConvertor() => Container.Bind<IJsonConvertor>().To<JsonConvertor>().AsSingle();

    private void ProgressService() => Container.Bind<IProgressService>().To<ProgressService>().AsSingle();

    private void BindAudioServiceLoader() => Container.Bind<IAudioService>().To<AudioService>().AsSingle();

    private void BindAssetProvider() => Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

    private void BindUIFactory() => Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

    private void BindSceenLoader()
    {
        SceenLoader sceenLoader = Container.InstantiatePrefabForComponent<SceenLoader>(_sceenLoader);
        Container.Bind<ISceenLoader>().FromInstance(sceenLoader).AsSingle();
    }

    private void BindInputService()
    {
        if (Application.isEditor)
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
        else
            Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
    }

    private void BindStateMachine() =>
        Container.Bind<StateMachine>().To<StateMachine>().AsSingle();

    private void BindGameBootstrapper()
    {
        GameBootstrapper gameBootstrapper = Container.InstantiatePrefabForComponent<GameBootstrapper>(_bootstrapperPrefab);
        Container.Bind<GameBootstrapper>().FromInstance(gameBootstrapper).AsSingle();
    }
}
