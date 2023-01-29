public class BootstrapState:  IState
{
    private readonly StateMachine _stateMachine;
    private readonly ISceenLoader _sceneLoader;
    
    public BootstrapState(StateMachine stateMachine, ISceenLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        _sceneLoader.Load(SceneEnum.Initialize, OnSceenLoaded);
    }

    private void OnSceenLoaded()
    {
        _stateMachine.Enter<GameLoopState>();
    }

    public void Exit()
    {

    }
}