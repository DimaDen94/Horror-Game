public class LoadLevelState : IPayloadState<string>
{
    private readonly StateMachine _stateMachine;
    private readonly ISceenLoader _sceneLoader;

    public LoadLevelState(StateMachine stateMachine,ISceenLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter(string level)
    {
        _sceneLoader.Load(level);
    }

    public void Exit()
    {

    }
}