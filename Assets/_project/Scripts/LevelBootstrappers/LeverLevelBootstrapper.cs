using System;
using UnityEngine;

public class LeverLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private WallWithHidingPlace _wallWithHidingPlace;
    [SerializeField] private LeverMechanism _leverMechanism;
    [SerializeField] private Chest _chest;
    [SerializeField] private ChestKey _chestKey;
    [SerializeField] private Lever _lever;
    [SerializeField] private ExitKey _exitKey;


    private new void Start()
    {
        base.Start();
        _chest.Construct(_toastService);
        _leverMechanism.Construct(_toastService);

        _chestKey.Lifted += OnChestKeyLifted;
        _chest.ChestOpened += OnChestOpened;
        _leverMechanism.MechanismFixed += OnMechanismFixed;
        _leverMechanism.MechanismActivated += OnMechanismActivated;
        _lever.Lifted += OnLeverLifted;
        _exitKey.Lifted += OnExitKeyLifted;
    }



    private void OnChestKeyLifted() => _analyticService.LeverLevelKeyLifted();

    private void OnChestOpened() => _analyticService.LeverLevelChestOpened();

    private void OnLeverLifted() => _analyticService.LeverLevelLeverLifted();

    private void OnMechanismFixed() => _analyticService.LeverLevelMechanismFixed();

    private void OnMechanismActivated()
    {
        TryOpenDamper();
        _analyticService.LeverLevelLeverActivate();
    }

    private void OnExitKeyLifted() => _analyticService.LeverLevelKeyLifted();

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
        _chestKey.Lifted -= OnChestKeyLifted;
        _chest.ChestOpened -= OnChestOpened;
        _leverMechanism.MechanismFixed -= OnMechanismFixed;
        _leverMechanism.MechanismActivated -= OnMechanismActivated;
        _lever.Lifted -= OnLeverLifted;
        _exitKey.Lifted -= OnExitKeyLifted;
    }
}
