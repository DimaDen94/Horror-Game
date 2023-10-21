using System.Collections;
using UnityEngine;

public class MainMenuState : IState
{
    private const float DestroyDelay = 1;

    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private IProgressService _progressService;
    private ICoroutineRunner _coroutineRunner;
    private MainMenuMediator _hud;

    public MainMenuState(StateMachine stateMachine, ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService, IProgressService progressService, ICoroutineRunner coroutineRunner)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _progressService = progressService;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
        Debug.Log("MainMenuState Enter");
        _sceneLoader.Load(SceneEnum.MainMenu, OnLoad);
    }

    private void OnLoad()
    {
        _audioService.PlayBackMusic(SoundEnum.MenuLoopMusic);
        _hud = _uiFactory.CreateMainMenuHud();
        _hud.Construct(_stateMachine, _audioService,_progressService, _coroutineRunner);
    }

    public void Exit()
    {
        _hud.Hide();
    }

}