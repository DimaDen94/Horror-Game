using System.Collections;
using UnityEngine;

public class PauseState : IState
{
    private const float DestroyDelay = 1;

    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private ICoroutineRunner _coroutineRunner;
    private IVibrationService _vibrationService;
    private PauseMenuMediator _hud;

    public PauseState(StateMachine stateMachine, ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner, IVibrationService vibrationService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _coroutineRunner = coroutineRunner;
        _vibrationService = vibrationService;
    }

    public void Enter()
    {
        Time.timeScale = 0;
        _hud = _uiFactory.CreatePauseMenu();
        _hud.Construct(_stateMachine, _audioService, _vibrationService);
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
        _hud.DestroyPanel();
    }
}