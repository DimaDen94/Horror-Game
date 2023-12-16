using System;
using UnityEngine;

public class Nightmare : InteractionObject
{
    private const string DieAnimationKey = "Die";
    private const string AttackAnimationKey = "Attack";

    public event Action Dead;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _loot;

    [SerializeField] private AudioSource _audioSourceAttack;
    [SerializeField] private AudioSource _audioSourceDied;

    private IToastService _toastService;


    public void Construct(IToastService toastService)
    {
        _toastService = toastService;
    }

    public override InteractionResponse TryUse(HeroSlot slot)
    {
        if (slot.Thing is Bottle && ((Bottle)slot.Thing).IsRed())
        {
            ((Bottle)slot.Thing).Throw(this);
            return InteractionResponse.Used;
        }
        _toastService.ShowToast(TranslatableKey.INeedToNeutralizeTheMonster);
        return InteractionResponse.Wrong;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hero>() != null)
        {
            _animator.Play(AttackAnimationKey);
            _audioSourceAttack.Play();
            transform.LookAt(other.transform);
            other.GetComponent<Hero>().Death(transform);
        }
    }

    public void Die()
    {
        _animator.Play(DieAnimationKey);
        _audioSourceDied.Play();
        GetComponent<Collider>().enabled = false;
        _loot.GetComponent<Collider>().enabled = true;
        Dead?.Invoke();
    }


}
