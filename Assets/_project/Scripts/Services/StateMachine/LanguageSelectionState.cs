using System.Collections;
using UnityEngine;

public class LanguageSelectionState : IState
{
    private StateMachine _stateMachine;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private ICoroutineRunner _coroutineRunner;
    private ILocalizationService _localizationService;

    private LanguageSelectionMediator _hud;
    private float DestroyDelay = 1f;

    public LanguageSelectionState(StateMachine stateMachine, IUIFactory uiFactory, IAudioService audioService, ICoroutineRunner coroutineRunner, ILocalizationService localizationService)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _coroutineRunner = coroutineRunner;
        _localizationService = localizationService;
    }

    public void Enter()
    {
        _hud = _uiFactory.CreateLanguageMenu();
        _hud.Construct(_audioService,  _localizationService, _uiFactory, _stateMachine);
    }

    public void Exit()
    {
        _hud.Hide();
        _coroutineRunner.StartCoroutine(DestroyPanel());
    }

    private IEnumerator DestroyPanel()
    {
        yield return new WaitForSeconds(DestroyDelay);
        if(_hud != null)
            _hud.DestroyPanel();
    }
}