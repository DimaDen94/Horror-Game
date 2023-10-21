using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    [Inject]
    private void Construct(StateMachine stateMachine, IUIFactory uiFactory, IAudioService audioService, ISceenLoader sceenLoader,IProgressService progressService, IVibrationService vibrationService)
    {
        _stateMachine = stateMachine;
        _stateMachine.Construct(sceenLoader, uiFactory,audioService, this, progressService, vibrationService);
    }

    private void Start()
    {
        _stateMachine.Enter<BootstrapState>();
    }
}
