public class BootstrapState:  IState
{
    private readonly StateMachine _stateMachine;
    private readonly ISceenLoader _sceneLoader;
    private readonly IInAppReviewService _inAppReviewService;
    private readonly ICoroutineRunner _coroutineRunner;

    public BootstrapState(StateMachine stateMachine, ISceenLoader sceneLoader, IInAppReviewService inAppReviewService, ICoroutineRunner coroutineRunner)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _inAppReviewService = inAppReviewService;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
        _sceneLoader.Load(SceneEnum.Initialize, OnSceenLoaded);
        _inAppReviewService.Init(_coroutineRunner);
    }

    private void OnSceenLoaded()
    {
        _stateMachine.Enter<MainMenuState>();
    }

    public void Exit()
    {

    }
}