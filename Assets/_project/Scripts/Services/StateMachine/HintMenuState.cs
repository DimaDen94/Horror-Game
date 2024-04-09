using System.Collections;
using UnityEngine;

public class HintMenuState : IState
{
    private const float DestroyDelay = 0.5f;

    private StateMachine _stateMachine;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private ICoroutineRunner _coroutineRunner;
    private IProgressService _progressService;
    private ILevelConfigHolder _configHolder;
    private ILocalizationService _localizationService;
    private IAccessLayer _accessLayer;
    private IMainThreadDispatcher _mainThreadDispatcher;
    private HintMenuMediator _hud;

    public HintMenuState(StateMachine stateMachine,  IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner,
        IProgressService progressService, ILevelConfigHolder configHolder, ILocalizationService localizationService, IAccessLayer accessLayer, IMainThreadDispatcher mainThreadDispatcher)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _coroutineRunner = coroutineRunner;
        _progressService = progressService;
        _configHolder = configHolder;
        _localizationService = localizationService;
        _accessLayer = accessLayer;
        _mainThreadDispatcher = mainThreadDispatcher;
    }

    public void Enter()
    {
        Time.timeScale = 0;
        _mainThreadDispatcher.Enqueue(CreateHud);
    }

    private void CreateHud()
    {
        _hud = _uiFactory.CreateHintMenu();
        _hud.Construct(_stateMachine, _audioService, _progressService, _configHolder, _localizationService, _accessLayer);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        _hud.Hide();
        _coroutineRunner.StartCoroutine(DestroyPanel());
    }

    private IEnumerator DestroyPanel()
    {
        yield return new WaitForSeconds(DestroyDelay);
        if (_hud != null)
            _hud.DestroyPanel();
    }
}