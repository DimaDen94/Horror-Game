using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TreeDemon : MonoBehaviour
{

    private const string AttackAnimationKey = "Attack";

    [SerializeField] private Animator _animator;

    [SerializeField] private AudioSource _audioSourceAttack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hero>() != null)
        {
            _animator.Play(AttackAnimationKey);
            _audioSourceAttack.Play();
            transform.LookAt(other.transform);
            _animator.transform.DOMove(other.transform.position,5);
            other.GetComponent<Hero>().Death(transform);
        }
    }
}
