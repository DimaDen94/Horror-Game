using UnityEngine;

public class MainMenuState : IState
{
    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private IProgressService _progressService;

    public MainMenuState(StateMachine stateMachine, ISceenLoader sceneLoader, IUIFactory uiFactory, IAudioService audioService, IProgressService progressService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _progressService = progressService;
    }

    public void Enter()
    {
        Debug.Log("MainMenuState Enter");
        _sceneLoader.Load(SceneEnum.MainMenu, OnLoad);
        _audioService.PlayBackMusic(SoundEnum.MenuLoopMusic);
    }

    private void OnLoad()
    {
        MainMenuMediator hud = _uiFactory.CreateMainMenuHud();
        hud.Construct(_stateMachine, _audioService,_progressService);
    }

    public void Exit()
    {
        
    }
}