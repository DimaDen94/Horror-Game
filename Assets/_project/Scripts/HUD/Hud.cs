using System;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private ActionButton _actionButton;
    [SerializeField] private ActionButton _dropButton;

    [SerializeField] private Button _menu;
    [SerializeField] private Button _hint;

    private StateMachine _stateMachine;
    private IAudioService _audioService;

    public void ShowActionButton() => _actionButton.Show();
    public void HideActionButton() => _actionButton.Hide();

    public void ShowDropButton() => _dropButton.Show();
    public void HideDropButton() => _dropButton.Hide();

    public void Construct(StateMachine stateMachine, IAudioService audioService) {
        _stateMachine = stateMachine;
        _audioService = audioService;
    }

    private void Start()
    {
        _menu.onClick.AddListener(OpenPauseMenu);
        _menu.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _hint.GetComponent<ButtonClickPlayer>().Construct(_audioService);
    }

    private void OpenPauseMenu()
    {
        _stateMachine.Enter<PauseState>();
    }
}
