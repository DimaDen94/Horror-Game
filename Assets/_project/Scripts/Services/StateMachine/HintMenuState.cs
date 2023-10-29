using System.Collections;
using UnityEngine;

public class HintMenuState : IState
{
    private const float DestroyDelay = 1;

    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private ICoroutineRunner _coroutineRunner;
    private IProgressService _progressService;
    private ILevelConfigHolder _configHolder;
    private HintMenuMediator _hud;

    public HintMenuState(StateMachine stateMachine, ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner, IProgressService progressService, ILevelConfigHolder configHolder)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _coroutineRunner = coroutineRunner;
        _progressService = progressService;
        _configHolder = configHolder;
    }

    public void Enter()
    {
        Time.timeScale = 0;
        _hud = _uiFactory.CreateHintMenu();
        _hud.Construct(_stateMachine, _audioService, _progressService, _configHolder);
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