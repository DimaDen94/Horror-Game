using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintMenuMediator : MonoBehaviour
{
    private const string HideStateName = "Hide";

    [SerializeField] private Button _close;
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _highlightButton;
    [SerializeField] private Button _enemySlowDownButton;
    [SerializeField] private Button _adButton;

    [SerializeField] private TextMeshProUGUI _highlightStateText;
    [SerializeField] private TextMeshProUGUI _enemySlowDownStateText;

    [SerializeField] private TextMeshProUGUI _hintText;

    [SerializeField] private List<TextMeshProTranslator> _translators;

    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IProgressService _progressService;
    private ILevelConfigHolder _configHolder;
    private ILocalizationService _localizationService;
    private IAccessLayer _accessLayer;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IProgressService progressService, ILevelConfigHolder configHolder, ILocalizationService localizationService,
        IAccessLayer accessLayer)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _progressService = progressService;
        _configHolder = configHolder;
        _localizationService = localizationService;
        _accessLayer = accessLayer;
    }


    public void Hide() => _animator?.Play(HideStateName);

    public void DestroyPanel()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        _close.onClick.AddListener(CloseDialog);

        _close.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _highlightButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _enemySlowDownButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _adButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);


        _highlightButton.onClick.AddListener(HighlightActivate);
        _enemySlowDownButton.onClick.AddListener(EnemySlowDownActivate);

        UpdateLocalization();

        _adButton.onClick.AddListener(_accessLayer.OnAdCheckboxClick);
        if (!_progressService.CanShowAd())
            HideCheckmark();

        ShowHintButtons();
        UpdateHintStates();

        _progressService.HintStateChanged += UpdateHintStates;
        _progressService.ShowAdStateChanged += HideCheckmark;

        ShowTextHint(true);
    }

    private void HideCheckmark() => _adButton.gameObject.SetActive(false);

    private void HighlightActivate() => _accessLayer.OnHintCheckBoxClick(HintEnum.HintHighlight);

    private void EnemySlowDownActivate() => _accessLayer.OnHintCheckBoxClick(HintEnum.EnemySlowDown);

    private void UpdateHintStates()
    {
        foreach (HintEnum item in _configHolder.GetLevelConfig(_progressService.GetCurrentLevel()).hints)
        {
            bool enable = _progressService.GetHintStates(_progressService.GetCurrentLevel(), item);

            switch (item)
            {
                case HintEnum.HintHighlight:
                    _highlightStateText.SetText(TextByState(enable));
                    break;
                case HintEnum.EnemySlowDown:
                    _enemySlowDownStateText.SetText(TextByState(enable));
                    break;
                default:
                    break;
            }

        }
    }

    private void ShowTextHint(bool enable)
    {
        if (enable)
            _hintText.SetText(HintText());
    }

    private void UpdateLocalization()
    {
        foreach (var item in _translators)
            item.Construct(_localizationService);
    }

    private string HintText() => _localizationService.GetTranslateByKey(_configHolder.GetLevelConfig(_progressService.GetCurrentLevel()).TextHintKey);

    private void ShowHintButtons()
    {
        foreach (var item in _configHolder.GetLevelConfig(_progressService.GetCurrentLevel()).hints)
        {
            switch (item)
            {
                case HintEnum.HintHighlight:
                    _highlightButton.gameObject.SetActive(true);
                    break;
                case HintEnum.EnemySlowDown:
                    _enemySlowDownButton.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    private void CloseDialog() => _stateMachine.Enter<GameLoopState>();

    private string TextByState(bool enable)
    {
        if (enable)
            return "ON";
        else
            return "OFF";
    }

    private void OnDestroy()
    {
        _progressService.HintStateChanged -= UpdateHintStates;
        _progressService.ShowAdStateChanged -= HideCheckmark;
    }
}
