using UnityEngine;

public class StairLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private MovingStairs _movingStairs;
    [SerializeField] private WeightPlatform[] _platforms;

    private void Start()
    {
        InitHero();
        _movingStairs.Construct(_platforms);
    }
}
