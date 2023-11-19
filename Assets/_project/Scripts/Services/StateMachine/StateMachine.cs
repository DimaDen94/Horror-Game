using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    private ICoroutineRunner _coroutineRunner;


    public void Construct(ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner, IProgressService progressService,
        IVibrationService vibrationService, IImageLoader imageLoader, ILocalizationService localizationService, ILevelConfigHolder configHolder,
        IAdvertisementService advertisementService, IAnalyticService analyticService, IAccessLayer accessLayer, IInAppReviewService inAppReviewService)
    {
        _coroutineRunner = coroutineRunner;
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, inAppReviewService, coroutineRunner),
            [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, uiFactory, audioService, progressService,coroutineRunner, localizationService, accessLayer),
            [typeof(SettingState)] = new SettingState(this, sceneLoader, uiFactory, audioService, coroutineRunner,vibrationService, progressService,localizationService,accessLayer),
            [typeof(LanguageSelectionState)] = new LanguageSelectionState(this, uiFactory, audioService, coroutineRunner, localizationService),
            [typeof(PauseState)] = new PauseState(this, uiFactory, audioService, coroutineRunner,vibrationService, localizationService, accessLayer, progressService),
            [typeof(HintMenuState)] = new HintMenuState(this, uiFactory, audioService, coroutineRunner, progressService, configHolder, localizationService, accessLayer),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, audioService, uiFactory, coroutineRunner, advertisementService, progressService, analyticService),
            [typeof(LevelCompletedState)] = new LevelCompletedState(this, audioService, uiFactory, coroutineRunner, imageLoader, localizationService, progressService),
            [typeof(MemoryState)] = new MemoryState(this,  audioService, uiFactory, coroutineRunner, imageLoader, localizationService, progressService, configHolder, analyticService),
            [typeof(DeadState)] = new DeadState(this, audioService, vibrationService, progressService),
            [typeof(GameCompletedState)] = new GameCompletedState(this, audioService, uiFactory, _coroutineRunner, progressService, localizationService),
            [typeof(GameLoopState)] = new GameLoopState(advertisementService, progressService)
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
