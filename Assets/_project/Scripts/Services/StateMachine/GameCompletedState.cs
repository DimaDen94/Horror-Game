using System.Collections;
using UnityEngine;

public class GameCompletedState : IState
{
    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IUIFactory _uiFactory;
    private ICoroutineRunner _coroutineRunner;

    public GameCompletedState(StateMachine stateMachine, IAudioService audioService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _uiFactory = uiFactory;
        _coroutineRunner = coroutineRunner;
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
        _audioService.PlayBackMusic(SoundEnum.RegularLoopMusic);
        _uiFactory.CreateFinishBlackout();
        yield return new WaitForSeconds(5f);
        _uiFactory.Blackout?.Daybreak();
        _stateMachine.Enter<MainMenuState>();
    }
}