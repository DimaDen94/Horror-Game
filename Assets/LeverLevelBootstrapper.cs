using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private WallWithHidingPlace _wallWithHidingPlace;
    [SerializeField] private LeverMechanism _leverMechanism;

    private void Start()
    {
        InitHero();
        _leverMechanism.MechanismActivated += _wallWithHidingPlace.OpenDamper;
    }

    private void OnDestroy()
    {
        _leverMechanism.MechanismActivated -= _wallWithHidingPlace.OpenDamper;
    }
}
