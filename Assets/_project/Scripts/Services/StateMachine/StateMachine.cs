using System;
using System.Collections.Generic;
using Zenject;

public class StateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    [Inject]
    private void Construct(ISceenLoader sceneLoader, IUIFactory uiFactory)
    {
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
            [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, uiFactory),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState :class, IPayloadState<TPayload>
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        return _states[typeof(TState)] as TState;
    }

}
