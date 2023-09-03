using System;
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
        _leverMechanism.MechanismActivated += TryOpenDamper;
    }

    private void TryOpenDamper()
    {
        if(_leverMechanism.IsActivated)
            _wallWithHidingPlace.OpenDamper();
        else
            _wallWithHidingPlace.CloseDamper();
    }

    private void OnDestroy()
    {
        _leverMechanism.MechanismActivated -= TryOpenDamper;
    }
}
