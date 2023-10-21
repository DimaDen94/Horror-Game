using System.Collections;
using UnityEngine;

public class SettingState : IState
{
    private const float DestroyDelay = 1;

    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private ICoroutineRunner _coroutineRunner;
    private IVibrationService _vibrationService;
    private SettingMenuMediator _hud;

    public SettingState(StateMachine stateMachine, ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService,
        ICoroutineRunner coroutineRunner, IVibrationService vibrationService)
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
        Debug.Log("SettingState Enter");
        _sceneLoader.Load(SceneEnum.MainMenu, OnLoad);
    }

    private void OnLoad()
    {
        _hud = _uiFactory.CreateSettingMenuHud();
        _hud.Construct(_stateMachine, _audioService,_vibrationService);
    }

    public void Exit()
    {
        _hud.Hide();
        _coroutineRunner.StartCoroutine(DestroyPanel());
    }

    private IEnumerator DestroyPanel()
    {
        yield return new WaitForSeconds(DestroyDelay);
        _hud.DestroyPanel();
    }
}