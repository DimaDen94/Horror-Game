using System.Collections;
using UnityEngine;
using Zenject;

public class Albino : InteractionObject
{
    private const string DieAnimationKey = "Die";
    private const string AttackAnimationKey = "Attack";

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _loot;
    private StateMachine _stateMachine;


    [Inject]
    private void Construct(StateMachine stateMachine) =>
        _stateMachine = stateMachine;

    public override void TryUse(HeroSlot slot)
    {
        if (slot.Thing is Bottle && ((Bottle)slot.Thing).IsRed())
        {
            ((Bottle)slot.Thing).Throw(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hero>() != null)
        {
            _animator.Play(AttackAnimationKey);
            transform.LookAt(other.transform);
            other.GetComponent<Hero>().Death(transform);
            StartCoroutine(RestartGame());
        }
    }

    public void Die()
    {
        _animator.Play(DieAnimationKey);
        GetComponent<Collider>().enabled = false;
        _loot.GetComponent<Collider>().enabled = true;
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2);
        _stateMachine.Enter<LoadLevelState, string>(LevelEnum.Level3.ToString());
    }
}
