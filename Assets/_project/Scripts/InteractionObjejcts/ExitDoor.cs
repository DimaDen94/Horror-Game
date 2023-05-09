using UnityEngine;
using Zenject;

public class ExitDoor : InteractionObject
{
    [SerializeField]private LevelEnum _nextLevel;
    [SerializeField]private AudioSource _audioSource;
    private StateMachine _stateMachine;
    private IUIFactory _uiFactory;

    [Inject]
    private void Constract(StateMachine stateMachine, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
    }

    public override bool TryUse(HeroSlot slot)
    {
        if (_nextLevel == LevelEnum.None)
            return false;

        if (slot.Thing is ExitKey)
        {
            _audioSource.Play();
            _uiFactory.CreateBlackout();
            Invoke("NextLevel", 0.5f);
            return true;

        }
        return false;
    }

    private void NextLevel()
    {
        _stateMachine.Enter<LoadLevelState, string>(_nextLevel.ToString());
    }
}
