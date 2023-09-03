using System;
using UnityEngine;

public class ClownLevelBootstrupper : LevelBootstrapper
{
    [SerializeField] private Gate _clownGate;
    [SerializeField] private Gate _lvlGate;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LeverMechanism _leverMechanism;

    private void Start()
    {
        InitHero();

        _leverMechanism.MechanismActivated += SwitchGates;
        _clownGate.GateOpened += StartHorrorStep;

        _enemy.GetComponent<EnemyStateMachine>().SetTarget(_heroMover.GetComponent<Hero>());
    }

    private void SwitchGates()
    {
        if (_leverMechanism.IsActivated)
        {
            _clownGate.OpenGate();
            _lvlGate.OpenGate();
        }else {
            _clownGate.CloseGate();
            _lvlGate.CloseGate();
        }
    }

    private void StartHorrorStep()
    {
        _enemy.GetComponent<FollowHeroTransition>().Follow();
        _audioService.PlayBackMusic(SoundEnum.HorrorLoopMusic);

    }

    private void OnDestroy()
    {
        _leverMechanism.MechanismActivated -= SwitchGates;
        _clownGate.GateOpened -= StartHorrorStep;
    }
}
