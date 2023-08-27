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

        _leverMechanism.MechanismActivated += _clownGate.OpenGate;
        _leverMechanism.MechanismActivated += _lvlGate.OpenGate;
        _clownGate.GateOpened += StartHorrorStep;

        _enemy.GetComponent<EnemyStateMachine>().SetTarget(_heroMover.GetComponent<Hero>());
    }

    private void StartHorrorStep()
    {
        _enemy.GetComponent<FollowHeroTransition>().Follow();
        _audioService.PlayBackMusic(SoundEnum.HorrorLoopMusic);

    }

    private void OnDestroy()
    {
        _leverMechanism.MechanismActivated -= _clownGate.OpenGate;
        _leverMechanism.MechanismActivated -= _lvlGate.OpenGate;
        _clownGate.GateOpened -= StartHorrorStep;
    }
}
