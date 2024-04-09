using UnityEngine;

public class BootstrapState:  IState
{
    private readonly StateMachine _stateMachine;
    private readonly ISceenLoader _sceneLoader;
    private readonly IInAppReviewService _inAppReviewService;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IPushNotificationService _pushNotificationService;
    private readonly IIAPService _iAPService;

    public BootstrapState(StateMachine stateMachine, ISceenLoader sceneLoader, IInAppReviewService inAppReviewService, ICoroutineRunner coroutineRunner,
        IPushNotificationService pushNotificationService, IIAPService iAPService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _inAppReviewService = inAppReviewService;
        _coroutineRunner = coroutineRunner;
        _pushNotificationService = pushNotificationService;
        _iAPService = iAPService;
    }

    public void Enter()
    {
        _iAPService.Initialize();
        _sceneLoader.Load(SceneEnum.Initialize, OnSceenLoaded);
        _inAppReviewService.Init(_coroutineRunner);
        _pushNotificationService.CreatePushNotification();
        Application.targetFrameRate = 60;
    }

    private void OnSceenLoaded()
    {
        _stateMachine.Enter<MainMenuState>();
    }

    public void Exit()
    {

    }
}