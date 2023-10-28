using System;
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
    [SerializeField] private Animator _animator;

    protected StateMachine _stateMachine;
    protected IAudioService _audioService;
    private IVibrationService _vibrationService;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IVibrationService vibrationService)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _vibrationService = vibrationService;
    }

    protected void Start()
    {
        _soundButton.onClick.AddListener(SoundTurn);
        _musicButton.onClick.AddListener(MusicTurn);
        _vibrationButton.onClick.AddListener(VibrationTurn);
        _menuButton.onClick.AddListener(CloseDialog);

        _soundButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _musicButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _vibrationButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _menuButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);


        UpdateButtons();
    }

    private void UpdateButtons()
    {
        _soundStateText.SetText(TextByState(_audioService.IsSoundEnable()));
        _musicStateText.SetText(TextByState(_audioService.IsMusicEnable()));
        _vibrationStateText.SetText(TextByState(_vibrationService.IsVibrationEnable()));
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
}
