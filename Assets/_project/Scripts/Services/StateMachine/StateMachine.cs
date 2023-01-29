using System;
using System.Collections.Generic;
using Zenject;

public class StateMachine
{
    private Dictionary<Type, IState> _states;
    private IState _activeState;

    [Inject]
    private void Construct(ISceenLoader sceneLoader)
    {
        _states = new Dictionary<Type, IState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
            [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader),
        };
    }

    public void Enter<TState>() where TState : IState
    {
        _activeState?.Exit();

        IState state = _states[typeof(TState)];
        _activeState = state;
        state.Enter();
    }
}
