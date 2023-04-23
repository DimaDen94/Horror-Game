public class LoadLevelState : IPayloadState<string>
{
    private readonly StateMachine _stateMachine;
    private readonly ISceenLoader _sceneLoader;
    private readonly IAudioService _audioService;
    private readonly IUIFactory _uiFactory;

    public LoadLevelState(StateMachine stateMachine, ISceenLoader sceneLoader, IAudioService audioService, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _audioService = audioService;
        _uiFactory = uiFactory;
    }

    public void Enter(string level)
    {
        _uiFactory.Blackout?.Daybreak();

        _sceneLoader.Load(level);
        _audioService.PlayBackMusic(SoundEnum.RegularLoopMusic);
    }

    public void Exit()
    {
        //BlackoutMediator blackout = _uiFactory.CreateBlackout();
        //blackout.Backout();
    }
}