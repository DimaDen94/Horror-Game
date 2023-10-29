using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintMenuMediator : MonoBehaviour
{
    private const string HideStateName = "Hide";

    [SerializeField] private Button _close;
    [SerializeField] private Animator _animator;

    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IProgressService _progressService;
    private ILevelConfigHolder _configHolder;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IProgressService progressService, ILevelConfigHolder configHolder)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _progressService = progressService;
        _configHolder = configHolder;
    }

    protected void Start()
    {
        _close.onClick.AddListener(CloseDialog);
    }


    public void Hide() => _animator?.Play(HideStateName);

    public void DestroyPanel()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }

    private void CloseDialog() => _stateMachine.Enter<GameLoopState>();
}
