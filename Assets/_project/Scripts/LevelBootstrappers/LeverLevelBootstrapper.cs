using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private WallWithHidingPlace _wallWithHidingPlace;
    [SerializeField] private LeverMechanism _leverMechanism;
    [SerializeField] private Chest _chest;


    private new void Start()
    {
        base.Start();
        _leverMechanism.MechanismActivated += TryOpenDamper;
        _chest.Construct(_toastService);
        _leverMechanism.Construct(_toastService);
    }

    private void TryOpenDamper()
    {
        if(_leverMechanism.IsActivated)
            _wallWithHidingPlace.OpenDamper();
        else
            _wallWithHidingPlace.CloseDamper();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _leverMechanism.MechanismActivated -= TryOpenDamper;
    }
}
