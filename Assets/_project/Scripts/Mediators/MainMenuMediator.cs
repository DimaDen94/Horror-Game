using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMediator : MonoBehaviour
{
    private const string HideStateName = "Hide";
    private const float DestroyPanelDelay = 1f;

    [SerializeField] private Animator _animator;

    [SerializeField] private Button _continue;
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _setting;
    [SerializeField] private Button _adOff;

    private StateMachine _stateMachine;
    private IProgressService _progressService;
    private ICoroutineRunner _coroutineRunner;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IProgressService progressService, ICoroutineRunner coroutineRunner) {
        _stateMachine = stateMachine;
        _progressService = progressService;
        _coroutineRunner = coroutineRunner;
        _continue.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _newGame.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _setting.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _adOff.GetComponent<ButtonClickPlayer>().Construct(audioService);

        _continue.onClick.AddListener(Continue);
        _newGame.onClick.AddListener(NewGame);
        _setting.onClick.AddListener(Setting);
    }

    public void Hide() => _animator?.Play(HideStateName);

    private void NewGame()
    {
        _progressService.ResetProgress();
        _stateMachine.Enter<LoadLevelState, string>(_progressService.GetCurrentLevel().ToString());
    }

    private void Continue()
    {
        _stateMachine.Enter<LoadLevelState, string>(_progressService.GetCurrentLevel().ToString());
    }

    private void Setting()
    {
        Hide();
        _stateMachine.Enter<SettingState>();
        _coroutineRunner.StartCoroutine(DestroyPanel());
    }

    private IEnumerator DestroyPanel()
    {
        yield return new WaitForSeconds(DestroyPanelDelay);
        Destroy(gameObject);
    }
}
