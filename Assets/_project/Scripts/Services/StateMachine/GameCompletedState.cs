using System.Collections;
using UnityEngine;

public class GameCompletedState : IState
{
    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IUIFactory _uiFactory;
    private ICoroutineRunner _coroutineRunner;
    private IProgressService _progressService;
    private ILocalizationService _localizationService;

    public GameCompletedState(StateMachine stateMachine, IAudioService audioService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner, IProgressService progressService,
        ILocalizationService localizationService)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _uiFactory = uiFactory;
        _coroutineRunner = coroutineRunner;
        _progressService = progressService;
        _localizationService = localizationService;
    }

    public void Enter()
    {
        _coroutineRunner.StartCoroutine(ShowEndScreen());
    }

    public void Exit()
    {

    }

    public IEnumerator ShowEndScreen()
    {
        _audioService.PlayBackMusic(SoundEnum.FinalMusic);
        FinalBlackoutMediator blackout = _uiFactory.CreateFinishBlackout();
        blackout.Backout();
        blackout.Construct(_localizationService);
        yield return new WaitForSeconds(20f);
        _uiFactory.Blackout?.Daybreak();
        _progressService.ResetProgress();
        _stateMachine.Enter<MainMenuState>();
    }
}