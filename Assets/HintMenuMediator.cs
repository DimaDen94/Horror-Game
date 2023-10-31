using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintMenuMediator : MonoBehaviour
{
    private const string HideStateName = "Hide";

    [SerializeField] private Button _close;
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _textHintButton;
    [SerializeField] private Button _highlightButton;
    [SerializeField] private Button _enemySlowDownButton;

    [SerializeField] private TextMeshProUGUI _textHintStateText;
    [SerializeField] private TextMeshProUGUI _highlightStateText;
    [SerializeField] private TextMeshProUGUI _enemySlowDownStateText;

    [SerializeField] private TextMeshProUGUI _hintText;

    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IProgressService _progressService;
    private ILevelConfigHolder _configHolder;
    private ILocalizationService _localizationService;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IProgressService progressService, ILevelConfigHolder configHolder, ILocalizationService localizationService)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _progressService = progressService;
        _configHolder = configHolder;
        _localizationService = localizationService;
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
        _textHintButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _highlightButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _enemySlowDownButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);

        _textHintButton.onClick.AddListener(TextHintActivate);
        _highlightButton.onClick.AddListener(HighlightActivate);
        _enemySlowDownButton.onClick.AddListener(EnemySlowDownActivate);

        ShowHintButtons();
        UpdateHintStates();

        _progressService.HintStateChanged += UpdateHintStates;
    }

    private void TextHintActivate() => ActiveHint(HintEnum.HintText);

    private void HighlightActivate() => ActiveHint(HintEnum.HintHighlight);

    private void EnemySlowDownActivate() => ActiveHint(HintEnum.EnemySlowDown);

    private void ActiveHint(HintEnum hintType)
    {
        if (!_progressService.GetHintStates(_progressService.GetCurrentLevel(), hintType))
            _progressService.SetHintActive(_progressService.GetCurrentLevel(), hintType);
    }

    private void UpdateHintStates()
    {
        foreach (HintEnum item in _configHolder.GetLevelConfig(_progressService.GetCurrentLevel()).hints)
        {
            bool enable = _progressService.GetHintStates(_progressService.GetCurrentLevel(), item);

            switch (item)
            {
                case HintEnum.HintText:
                    _textHintStateText.SetText(TextByState(enable));
                    if (enable)
                        _hintText.SetText(HintText());
                    break;
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

    private string HintText() => _localizationService.GetTranslateByKey(_configHolder.GetLevelConfig(_progressService.GetCurrentLevel()).TextHintKey);

    private void ShowHintButtons()
    {
        foreach (var item in _configHolder.GetLevelConfig(_progressService.GetCurrentLevel()).hints)
        {
            switch (item)
            {
                case HintEnum.HintText:
                    _textHintButton.gameObject.SetActive(true);
                    break;
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
    }
}
