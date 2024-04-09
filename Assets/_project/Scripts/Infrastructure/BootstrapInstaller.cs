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
        BindAnalyticService();
        BindInAppReviewService();
        BindAdvertisementService();
        BindIAPProvider();
        BindJsonConvertor();
        BindAssetProvider();
        BingLevelConfigService();
        BindImageLoader();
        BindProgressService();
        BindIAPService();
        BindLocalizationService();
        BindNotificationService();
        BindStateMachine();
        BindAccessLayer();
        BindSceenLoader();
        BindAudioService();
        BindVibrationService();
        BindInputService();
        BindUIFactory();
        BindToastService();
        BindGameFactory();
        BindGameBootstrapper();

    }

    private void BindPlayerPrefsService() => Container.Bind<IPlayerPrefsService>().To<PlayerPrefsService>().AsSingle();

    private void BindAnalyticService() => Container.Bind<IAnalyticService>().To<FirebaseAnalyticService>().AsSingle();

    private void BindInAppReviewService() => Container.Bind<IInAppReviewService>().To<AndroidInAppReviewService>().AsSingle();

    private void BindAdvertisementService() => Container.Bind<IAdvertisementService>().To<AdMobAdvertisementService>().AsSingle();

    private void BindIAPProvider() => Container.Bind<IAPProvider>().To<IAPProvider>().AsSingle();

    private void BindJsonConvertor() => Container.Bind<IJsonConvertor>().To<JsonConvertor>().AsSingle();

    private void BindAssetProvider() => Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

    private void BingLevelConfigService() => Container.Bind<ILevelConfigHolder>().To<LevelConfigService>().AsSingle();

    private void BindImageLoader() => Container.Bind<IImageLoader>().To<ImageLoader>().AsSingle();

    private void BindProgressService() => Container.Bind<IProgressService>().To<ProgressService>().AsSingle();

    private void BindIAPService() => Container.Bind<IIAPService>().To<IAPService>().AsSingle();

    private void BindLocalizationService() => Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();

    private void BindNotificationService() => Container.Bind<IPushNotificationService>().To<PushNotificationService>().AsSingle();


    private void BindAudioService() => Container.Bind<IAudioService>().To<AudioService>().AsSingle();

    private void BindVibrationService() => Container.Bind<IVibrationService>().To<VibrationService>().AsSingle();

    private void BindUIFactory() => Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

    private void BindToastService() => Container.Bind<IToastService>().To<ToastService>().AsSingle();

    private void BindAccessLayer() => Container.Bind<IAccessLayer>().To<AccessLayer>().AsSingle();

    private void BindGameFactory() => Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

    private void BindSceenLoader()
    {
        SceenLoader sceenLoader = Container.InstantiatePrefabForComponent<SceenLoader>(_sceenLoader);
        Container.Bind<ISceenLoader>().FromInstance(sceenLoader).AsSingle();
    }

    private void BindInputService()
    {
        //if (Application.isEditor)
            //Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
        //else
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
