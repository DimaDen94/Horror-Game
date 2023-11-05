using UnityEngine;

public class LabyrinthLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private Chest _chest;
    [SerializeField] private RedChest _chestRed;
    [SerializeField] private BlueChest _chestBlue;

    private new void Start()
    {
        base.Start();
        _chest.Construct(_toastService);
        _chestRed.Construct(_toastService);
        _chestBlue.Construct(_toastService);
    }
}
