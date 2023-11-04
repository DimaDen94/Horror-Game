using UnityEngine;

public class FireAltarLevelBootstrapper : LevelBootstrapper
{
    [SerializeField] private DestroyedBarrierForEnemy _barrier;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LiftedThing _torch;

    private new void Start()
    {
        base.Start();
        _torch.Lifted += StartHorrorStep;
        _enemy.GetComponent<EnemyStateMachine>().SetTarget(_barrier.transform);
        _barrier.BarrierDestroyed += FollowHero;
    }

    protected override void EnemySlowDown()
    {
        _enemy.SlowDown();
    }

    private void FollowHero()
    {
        _enemy.GetComponent<EnemyStateMachine>().SetTarget(_hero.transform);
        _enemy.GetComponent<FollowTargetTransition>().Follow();
    }

    private void StartHorrorStep()
    {
        _enemy.GetComponent<FollowTargetTransition>().Follow();
        _audioService.PlayBackMusic(SoundEnum.HorrorLoopMusic);

    }

    protected override void StopLevel()
    {
        base.StopLevel();
        Destroy(_enemy.gameObject);
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        _barrier.BarrierDestroyed -= FollowHero;
    }
}
