using System.Collections;
using UnityEngine;
using Zenject;

public class Albino : InteractionObject
{
    private const string DieAnimationKey = "Die";
    private const string AttackAnimationKey = "Attack";

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _loot;

    [SerializeField] private AudioSource _audioSourceAttack;
    [SerializeField] private AudioSource _audioSourceDied;

    private StateMachine _stateMachine;
    private IUIFactory _uiFactory;

    [Inject]
    private void Construct(StateMachine stateMachine, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
    }

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is Bottle && ((Bottle)slot.Thing).IsRed())
        {
            ((Bottle)slot.Thing).Throw(this);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hero>() != null)
        {
            _animator.Play(AttackAnimationKey);
            _audioSourceAttack.Play();
            transform.LookAt(other.transform);
            other.GetComponent<Hero>().Death(transform);
            StartCoroutine(RestartGame());
        }
    }

    public void Die()
    {
        _animator.Play(DieAnimationKey);
        _audioSourceDied.Play();
        GetComponent<Collider>().enabled = false;
        _loot.GetComponent<Collider>().enabled = true;
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1);
        _uiFactory.CreateBlackout();
        yield return new WaitForSeconds(0.5f);
        _stateMachine.Enter<LoadLevelState, string>(LevelEnum.Level3.ToString());
    }
}
