using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StateMachine stateMachine, IUIFactory uiFactory, IAudioService audioService, ISceenLoader sceenLoader)
    {
        _stateMachine = stateMachine;
        _stateMachine.Construct(sceenLoader, uiFactory,audioService, this);
    }

    private void Start()
    {
        _stateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}
