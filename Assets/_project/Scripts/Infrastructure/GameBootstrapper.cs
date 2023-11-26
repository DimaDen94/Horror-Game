using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    [Inject]
    private void Construct(StateMachine stateMachine, IUIFactory uiFactory, IAudioService audioService, ISceenLoader sceenLoader,IProgressService progressService,
        IVibrationService vibrationService, IImageLoader imageLoader, ILocalizationService localizationService, ILevelConfigHolder levelConfigHolder,IAdvertisementService advertisementService,
        IAnalyticService analyticService, IAccessLayer accessLayer, IInAppReviewService inAppReviewService, IPushNotificationService pushNotificationService)
    {
        _stateMachine = stateMachine;
        _stateMachine.Construct(sceenLoader, uiFactory,audioService, this, progressService, vibrationService, imageLoader, localizationService,
            levelConfigHolder, advertisementService, analyticService, accessLayer, inAppReviewService, pushNotificationService);
    }

    private void Start()
    {
        _stateMachine.Enter<BootstrapState>();
    }
}
