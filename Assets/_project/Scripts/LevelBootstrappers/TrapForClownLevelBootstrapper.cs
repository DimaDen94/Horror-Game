using UnityEngine;

public class TrapForClownLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private LeverMechanism _clownLeverMechanism;
    [SerializeField] private LeverMechanism _firstLeverMechanism;
    [SerializeField] private LeverMechanism _secondLeverMechanism;

    [SerializeField] private Gate _clownGate;
    [SerializeField] private Gate _firstGate;
    [SerializeField] private Gate _secondGate;

    [SerializeField] private Enemy _enemy;

    private new void Start()
    {
        base.Start();
        _enemy.GetComponent<EnemyStateMachine>().SetTarget(_hero.transform);

        _clownLeverMechanism.MechanismActivated += TryOpenClownGate;
        _firstLeverMechanism.MechanismActivated += TryOpenFirstGate;
        _secondLeverMechanism.MechanismActivated += TryOpenSecondGate;

        _clownGate.GateOpened += FollowEnemy;

    }

    protected override void EnemySlowDown()
    {
        _enemy.SlowDown();
    }

    private void FollowEnemy()
    {
        _audioService.PlayBackMusic(SoundEnum.HorrorLoopMusic);
        _enemy.GetComponent<FollowTargetTransition>().Follow();
    }

    private void TryOpenClownGate()
    {
        if (_clownLeverMechanism.IsActivated)
            _clownGate.OpenGate();
        else
            _clownGate.CloseGate();
    }

    private void TryOpenFirstGate()
    {
        if (_firstLeverMechanism.IsActivated)
            _firstGate.OpenGate();
        else
            _firstGate.CloseGate();
    }

    private void TryOpenSecondGate()
    {
        if (_secondLeverMechanism.IsActivated)
            _secondGate.OpenGate();
        else
            _secondGate.CloseGate();
    }
    private new void OnDestroy()
    {
        base.OnDestroy();

        _clownLeverMechanism.MechanismActivated -= TryOpenClownGate;
        _firstLeverMechanism.MechanismActivated -= TryOpenFirstGate;
        _secondLeverMechanism.MechanismActivated -= TryOpenSecondGate;

        _clownGate.GateOpened -= FollowEnemy;
    }
}
