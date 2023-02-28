public class GameLoopState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly ISceenLoader _sceneLoader;

    public GameLoopState(StateMachine stateMachine,ISceenLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        _sceneLoader.Load(SceneEnum.Game);
    }

    public void Exit()
    {

    }
}