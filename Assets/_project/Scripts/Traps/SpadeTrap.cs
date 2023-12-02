using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class SpadeTrap : MonoBehaviour
{
    [SerializeField] private Transform _spades;

    [SerializeField] private AudioSource _audioSourceAttack;
    private StateMachine _stateMachine;
    private IUIFactory _uiFactory;

    [Inject]
    private void Construct(StateMachine stateMachine, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hero>() != null)
        {
            _spades.DOMoveY(-1,1);
            _audioSourceAttack.Play();
            other.GetComponent<Hero>().Death(transform);
        }
    }
}
