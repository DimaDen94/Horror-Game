using System.Collections;
using UnityEngine;

public class HintMenuState : IState
{
    private const float DestroyDelay = 1;

    private StateMachine _stateMachine;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private ICoroutineRunner _coroutineRunner;
    private IProgressService _progressService;
    private ILevelConfigHolder _configHolder;
    private ILocalizationService _localizationService;
    private IAccessLayer _accessLayer;
    private HintMenuMediator _hud;

    public HintMenuState(StateMachine stateMachine,  IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner,
        IProgressService progressService, ILevelConfigHolder configHolder, ILocalizationService localizationService, IAccessLayer accessLayer)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _coroutineRunner = coroutineRunner;
        _progressService = progressService;
        _configHolder = configHolder;
        _localizationService = localizationService;
        _accessLayer = accessLayer;
    }

    public void Enter()
    {
        Time.timeScale = 0;
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