using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadState : IState
{
    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IUIFactory _uiFactory;
    private ICoroutineRunner _coroutineRunner;

    public DeadState(StateMachine stateMachine, IAudioService audioService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _uiFactory = uiFactory;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
        _coroutineRunner.StartCoroutine(RestartGame());
    }

    public void Exit()
    {
        
    }

    public IEnumerator RestartGame()
    {
        _audioService.PlayAudio(SoundEnum.Wrong);
        yield return new WaitForSeconds(1);
        _uiFactory.CreateBlackout();
        yield return new WaitForSeconds(0.5f);
        _stateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
    }
}