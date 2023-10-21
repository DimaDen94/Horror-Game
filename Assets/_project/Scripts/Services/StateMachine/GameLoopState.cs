public class GameLoopState : IState
{
    private StateMachine _stateMachine;

    public GameLoopState(StateMachine stateMachine)
    {
       _stateMachine = stateMachine;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }
}