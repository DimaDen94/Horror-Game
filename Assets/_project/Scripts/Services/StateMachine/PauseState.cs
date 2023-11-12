using System.Collections;
using UnityEngine;

public class PauseState : IState
{
    private const float DestroyDelay = 1;

    private StateMachine _stateMachine;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private ICoroutineRunner _coroutineRunner;
    private IVibrationService _vibrationService;
    private ILocalizationService _localizationService;
    private IAccessLayer _accessLayer;
    private IProgressService _progressService;
    private PauseMenuMediator _hud;

    public PauseState(StateMachine stateMachine, IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner,
        IVibrationService vibrationService, ILocalizationService localizationService, IAccessLayer accessLayer, IProgressService progressService)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _coroutineRunner = coroutineRunner;
        _vibrationService = vibrationService;
        _localizationService = localizationService;
        _accessLayer = accessLayer;
        _progressService = progressService;
    }

    public void Enter()
    {
        Time.timeScale = 0;
        _hud = _uiFactory.CreatePauseMenu();
        _hud.Construct(_stateMachine, _audioService, _vibrationService, _localizationService, _accessLayer, _progressService);
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
        if(_hud!=null)
            _hud.DestroyPanel();
    }
}