using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        _stateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}
