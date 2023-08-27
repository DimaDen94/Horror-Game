using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    private ICoroutineRunner _coroutineRunner;


    public void Construct(ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
            [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, uiFactory, audioService),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, audioService, uiFactory),
            [typeof(DeadState)] = new DeadState(this, audioService, uiFactory, _coroutineRunner),
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
