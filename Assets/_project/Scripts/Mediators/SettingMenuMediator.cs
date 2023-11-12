using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuMediator : MonoBehaviour
{
    private const string HideStateName = "Hide";

    [SerializeField] private Button _soundButton;
    [SerializeField] private TextMeshProUGUI _soundStateText;
    [SerializeField] private Button _musicButton;
    [SerializeField] private TextMeshProUGUI _musicStateText;
    [SerializeField] private Button _vibrationButton;
    [SerializeField] private TextMeshProUGUI _vibrationStateText;
    [SerializeField] private Button _menuButton;

    [SerializeField] private Button _adButton;
    [SerializeField] private GameObject _checkmark;

    [SerializeField] private List<TextMeshProTranslator> _translators;

    [SerializeField] private Animator _animator;

    protected StateMachine _stateMachine;
    protected IAudioService _audioService;
    private IVibrationService _vibrationService;
    private ILocalizationService _localizationService;
    private IAccessLayer _accessLayer;
    private IProgressService _progressService;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IVibrationService vibrationService, ILocalizationService localizationService,
        IAccessLayer accessLayer, IProgressService progressService)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _vibrationService = vibrationService;
        _localizationService = localizationService;
        _accessLayer = accessLayer;
        _progressService = progressService;
    }

    protected void Start()
    {
        _soundButton.onClick.AddListener(SoundTurn);
        _musicButton.onClick.AddListener(MusicTurn);
        _vibrationButton.onClick.AddListener(VibrationTurn);
        _menuButton.onClick.AddListener(CloseDialog);
        _adButton.onClick.AddListener(_accessLayer.OnAdCheckboxClick);

        _soundButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _musicButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _vibrationButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _menuButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _adButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);

        if (!_progressService.CanShowAd())
            HideCheckmark();

        _progressService.ShowAdStateChanged += HideCheckmark;


        UpdateButtons();
        UpdateLocalization();
    }


    private void HideCheckmark() => _checkmark.SetActive(false);

    private void UpdateButtons()
    {
        _soundStateText.SetText(TextByState(_audioService.IsSoundEnable()));
        _musicStateText.SetText(TextByState(_audioService.IsMusicEnable()));
        _vibrationStateText.SetText(TextByState(_vibrationService.IsVibrationEnable()));
    }

    private void UpdateLocalization()
    {
        foreach (var item in _translators)
            item.Construct(_localizationService);
    }

    public void Hide() => _animator?.Play(HideStateName);

    public void DestroyPanel()
    {
        if(gameObject!=null)
            Destroy(gameObject);
    }

    private void CloseDialog() => _stateMachine.Enter<MainMenuState>();

    private void VibrationTurn()
    {
        _vibrationService.SwitchEnable();
        _vibrationStateText.SetText(TextByState(_vibrationService.IsVibrationEnable()));
    }

    private void MusicTurn()
    {
        _audioService.MusicEnable(!_audioService.IsMusicEnable());
        _musicStateText.SetText(TextByState(_audioService.IsMusicEnable()));
    }

    private void SoundTurn()
    {
        _audioService.SoundEnable(!_audioService.IsSoundEnable());
        _soundStateText.SetText(TextByState(_audioService.IsSoundEnable()));
    }

    private string TextByState(bool enable)
    {
        if (enable)
            return "ON";
        else
            return "OFF";
    }


    private void OnDestroy()
    {
        _progressService.ShowAdStateChanged -= HideCheckmark;
    }
}
