using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMediator : MonoBehaviour
{
    private const string HideStateName = "Hide";
    private const float DestroyPanelDelay = 1f;

    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _animatorLang;

    [SerializeField] private Button _continue;
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _setting;
    [SerializeField] private Button _adButton;
    [SerializeField] private Button _language;
    [SerializeField] private GameObject _checkmark;

    [SerializeField] private List<TextMeshProTranslator> _translators;

    private StateMachine _stateMachine;
    private IProgressService _progressService;
    private ICoroutineRunner _coroutineRunner;
    private ILocalizationService _localizationService;
    private IAudioService _audioService;
    private IAccessLayer _accessLayer;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IProgressService progressService, ICoroutineRunner coroutineRunner,
        ILocalizationService localizationService, IAccessLayer accessLayer) {
        _stateMachine = stateMachine;
        _progressService = progressService;
        _coroutineRunner = coroutineRunner;
        _localizationService = localizationService;
        _audioService = audioService;
        _accessLayer = accessLayer;
    }

    private void Start()
    {
        _continue.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _newGame.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _setting.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _adButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _language.GetComponent<ButtonClickPlayer>().Construct(_audioService);

        _language.GetComponent<Image>().sprite = _localizationService.GetCurrentLanguageIcon();

        _continue.onClick.AddListener(Continue);
        _newGame.onClick.AddListener(NewGame);
        _setting.onClick.AddListener(Setting);
        _adButton.onClick.AddListener(_accessLayer.OnAdCheckboxClick);
        _language.onClick.AddListener(ShowLanguageList);

        if (!_progressService.CanShowAd())
            HideCheckmark();

        _progressService.ShowAdStateChanged += HideCheckmark;

        UpdateLocalization();
    }

    private void UpdateLocalization() {
        foreach (var item in _translators)
            item.Construct(_localizationService);
    }

    private void HideCheckmark() => _checkmark.SetActive(false);

    public void Hide()
    {
        _animator?.Play(HideStateName);
        _animatorLang?.Play(HideStateName);
    }

    private void NewGame()
    {
        _progressService.ResetProgress();
        _stateMachine.Enter<LoadLevelState, LevelEnum>(_progressService.GetCurrentLevel());
    }

    private void Continue() => _stateMachine.Enter<LoadLevelState, LevelEnum>(_progressService.GetCurrentLevel());

    private void Setting() => _stateMachine.Enter<SettingState>();

    private void ShowLanguageList() => _stateMachine.Enter<LanguageSelectionState>();

    public IEnumerator DestroyPanel()
    {
        yield return new WaitForSeconds(DestroyPanelDelay);
        Destroy(gameObject);
    }

    private void OnDestroy() => _progressService.ShowAdStateChanged -= HideCheckmark;
}
