using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner, IMainThreadDispatcher
{
    private StateMachine _stateMachine;
    private static readonly Queue<Action> _executionQueue = new Queue<Action>();

    private void Awake() => DontDestroyOnLoad(this.gameObject);

    [Inject]
    private void Construct(StateMachine stateMachine, IUIFactory uiFactory, IAudioService audioService, ISceenLoader sceenLoader, IProgressService progressService,
        IVibrationService vibrationService, IImageLoader imageLoader, ILocalizationService localizationService, ILevelConfigHolder levelConfigHolder, IAdvertisementService advertisementService,
        IAnalyticService analyticService, IAccessLayer accessLayer, IInAppReviewService inAppReviewService, IPushNotificationService pushNotificationService, IIAPService iAPService)
    {
        _stateMachine = stateMachine;
        _stateMachine.Construct(sceenLoader, uiFactory, audioService, this, progressService, vibrationService, imageLoader, localizationService,
            levelConfigHolder, advertisementService, analyticService, accessLayer, inAppReviewService, pushNotificationService, this, iAPService);
    }

    private void Start() => _stateMachine.Enter<BootstrapState>();


    public void Update()
    {
        lock (_executionQueue)
        {
            while (_executionQueue.Count > 0)
            {
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    public void Enqueue(Action action)
    {
        lock (_executionQueue)
        {
            _executionQueue.Enqueue(action);
        }
    }

}
