using UnityEngine;

public class DragonLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private Chest _chest;
    [SerializeField] private Albino _enemy;

    private new void Start()
    {
        base.Start();
        _chest.Construct(_toastService);
        _enemy.Construct(_toastService);
        _enemy.Dead += OnDragonDead;
    }

    private void OnDragonDead()
    {
        _inAppReviewService.RequestAppReview();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _enemy.Dead -= OnDragonDead;
    }
}
