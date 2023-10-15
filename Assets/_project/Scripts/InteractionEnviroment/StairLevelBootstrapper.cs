using UnityEngine;

public class StairLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private MovingStairs _movingStairs;
    [SerializeField] private WeightPlatform[] _platforms;

    private new void Start()
    {
        base.Start();
        _movingStairs.Construct(_platforms);
    }
}
