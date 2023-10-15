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
    private void Construct(StateMachine stateMachine, IUIFactory uiFactory, IAudioService audioService, ISceenLoader sceenLoader,IProgressService progressService)
    {
        _stateMachine = stateMachine;
        _stateMachine.Construct(sceenLoader, uiFactory,audioService, this, progressService);
    }

    private void Start()
    {
        _stateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}
