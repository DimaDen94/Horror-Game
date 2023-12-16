using UnityEngine;

public class FirstClownLevelBootstrupper : LevelBootstrapper
{
    [SerializeField] private Gate _firstGate;
    [SerializeField] private Gate _clownGate;
    [SerializeField] private Gate _lvlGate;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LeverMechanism _firstLeverMechanism;
    [SerializeField] private LeverMechanism _lastLeverMechanism;

    private new void Start()
    {
        base.Start();
        _firstLeverMechanism.MechanismActivated += SwitchStartGates;
        _lastLeverMechanism.MechanismActivated += SwitchGates;
        _clownGate.GateOpened += StartHorrorStep;
        _enemy.GetComponent<EnemyStateMachine>().SetTarget(_hero.transform);
        _lastLeverMechanism.Construct(_toastService);
    }

    private void SwitchStartGates()
    {
        if (_firstLeverMechanism.IsActivated)
            _firstGate.OpenGate();
        else
            _firstGate.CloseGate();
    }

    protected override void EnemySlowDown()
    {
        _enemy.SlowDown();
    }

    private void SwitchGates()
    {
        if (_lastLeverMechanism.IsActivated)
        {
            _clownGate.OpenGate();
            _lvlGate.OpenGate();
        }
        else
        {
            _clownGate.CloseGate();
            _lvlGate.CloseGate();
        }
    }

    private void StartHorrorStep()
    {
        _enemy.GetComponent<FollowTargetTransition>().Follow();
        _audioService.PlayBackMusic(SoundEnum.HorrorLoopMusic);

    }

    protected override void StopLevel()
    {
        base.StopLevel();
        Destroy(_enemy.gameObject);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _lastLeverMechanism.MechanismActivated -= SwitchGates;
        _clownGate.GateOpened -= StartHorrorStep;
    }
}
