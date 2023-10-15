using UnityEngine;
using UnityEngine.AI;

public class FinalLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LeverMechanism _startLevelar;
    [SerializeField] private Gate _startGate;
    [SerializeField] private LeverMechanism _bridgeLevelar;
    [SerializeField] private LiftedBridge _bridge;
    [SerializeField] private NavMeshSurface _surface;

    [SerializeField] private LeverMechanism _lastLevelar;
    [SerializeField] private Gate _lastGate;

    [SerializeField] private FinishGate _finishGate;

    private new void Start()
    {
        base.Start();
        _startLevelar.MechanismActivated += OnStartLevelar;
        _bridgeLevelar.MechanismActivated += MoveBridge;
        _enemy.GetComponent<EnemyStateMachine>().SetTarget(_heroMover.transform);

        _bridge.BridgeCompleted += OnBridgeCompleted;
        _lastLevelar.MechanismActivated += OnFinishLevelar;
        _finishGate.PlayerOnFinish += OnPlayerOnFinish;
    }

    private void OnPlayerOnFinish()
    {
        _stateMachine.Enter<GameCompletedState>();
    }

    private void OnFinishLevelar()
    {
        _lastGate.CloseGate();
    }

    private void OnBridgeCompleted()
    {
        _surface.BuildNavMesh();
    }

    private void MoveBridge()
    {
        _bridge.LiftUp();
    }

    private void OnStartLevelar()
    {
        _enemy.GetComponent<FollowTargetTransition>().Follow();
        _startGate.OpenGate();
    }

    private void OnDestroy()
    {
        _startLevelar.MechanismActivated -= OnStartLevelar;
        _bridgeLevelar.MechanismActivated -= MoveBridge;
    }

 
}
