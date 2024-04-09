using System;
using UnityEngine;

public class StairLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private MovingStairs _movingStairs;
    [SerializeField] private WeightPlatform[] _platforms;
    [SerializeField] private AudioSource _laught;
    [SerializeField] private AudioSource _scream;

    private int _weightCount = 0;
    private new void Start()
    {
        base.Start();
        _movingStairs.Construct(_platforms);
        foreach (var platform in _platforms)
        {
            platform.WeightAdded += OnWaightAdded;
        }
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        foreach (var platform in _platforms)
        {
            platform.WeightAdded -= OnWaightAdded;
        }
    }

    private void OnWaightAdded(string name)
    {
        _weightCount++;
        if (_weightCount == 2)
            _laught.Play();
        if (_weightCount == 5)
        {
            _scream.Play();
            _uiFactory.CreateBlackout();
            _uiFactory.Blackout.Daybreak();
        }
        _analyticService.StairLevelCollectedWeight(_weightCount, name);
    }
}
