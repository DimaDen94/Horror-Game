using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuMediator : SettingMenuMediator
{
    [SerializeField] private Button _continueButton;

    private new void Start()
    {
        base.Start();
        _continueButton.onClick.AddListener(Continue);
        _continueButton.GetComponent<ButtonClickPlayer>().Construct(_audioService);
    }

    private void Continue()
    {
        _stateMachine.Enter<GameLoopState>();
    }
}