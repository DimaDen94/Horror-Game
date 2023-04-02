using UnityEngine;
using Zenject;

public class ExitDoor : InteractionObject
{
    [SerializeField]private LevelEnum _nextLevel;
    private StateMachine _stateMachine;

    [Inject]
    private void Constract(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void TryUse(HeroSlot slot)
    {
        if (_nextLevel == LevelEnum.None)
            return;

        if (slot.Thing is ExitKey) {
            _stateMachine.Enter<LoadLevelState, string>(_nextLevel.ToString());
        }
    }
}
