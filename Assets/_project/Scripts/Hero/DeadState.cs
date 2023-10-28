public class DeadState : IState
{
    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IVibrationService _vibrationService;
    private IProgressService _progressService;

    public DeadState(StateMachine stateMachine, IAudioService audioService, IVibrationService vibrationService, IProgressService progressService)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _vibrationService = vibrationService;
        _progressService = progressService;
    }

    public void Enter()
    {
        _audioService.PlayAudio(SoundEnum.Wrong);
        _vibrationService.TryVibration();
        _stateMachine.Enter<LoadLevelState, LevelEnum>(_progressService.GetCurrentLevel());
    }

    public void Exit()
    {
        
    }
}