using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private List<TextMeshProTranslator> _translators;

    private StateMachine _stateMachine;
    private IProgressService _progressService;
    private ICoroutineRunner _coroutineRunner;
    private ILocalizationService _localizationService;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IProgressService progressService, ICoroutineRunner coroutineRunner, ILocalizationService localizationService) {
        _stateMachine = stateMachine;
        _progressService = progressService;
        _coroutineRunner = coroutineRunner;
        _localizationService = localizationService;

        _continue.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _newGame.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _setting.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _adOff.GetComponent<ButtonClickPlayer>().Construct(audioService);

        _continue.onClick.AddListener(Continue);
        _newGame.onClick.AddListener(NewGame);
        _setting.onClick.AddListener(Setting);

        UpdateLocalization();
    }

    private void UpdateLocalization() {
        foreach (var item in _translators)
            item.Construct(_localizationService);
    }

    public void Hide() => _animator?.Play(HideStateName);

    private void NewGame()
    {
        _progressService.ResetProgress();
        _stateMachine.Enter<LoadLevelState, LevelEnum>(_progressService.GetCurrentLevel());
    }

    private void Continue()
    {
        _stateMachine.Enter<LoadLevelState, LevelEnum>(_progressService.GetCurrentLevel());
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
