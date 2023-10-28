using System.Collections;
using UnityEngine;

public class MainMenuState : IState
{
    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private IProgressService _progressService;
    private ICoroutineRunner _coroutineRunner;
    private ILocalizationService _localizationService;
    private MainMenuMediator _hud;

    public MainMenuState(StateMachine stateMachine, ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService, IProgressService progressService,
        ICoroutineRunner coroutineRunner, ILocalizationService localizationService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _progressService = progressService;
        _coroutineRunner = coroutineRunner;
        _localizationService = localizationService;
    }

    public void Enter()
    {
        _sceneLoader.Load(SceneEnum.MainMenu, OnLoad);
    }

    private void OnLoad()
    {
        _audioService.PlayBackMusic(SoundEnum.MenuLoopMusic);
        _hud = _uiFactory.CreateMainMenuHud();
        _hud.Construct(_stateMachine, _audioService,_progressService, _coroutineRunner, _localizationService);
    }

    public void Exit()
    {
        _hud.Hide();
    }

}