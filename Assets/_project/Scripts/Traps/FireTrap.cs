using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private GameObject _fire;

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
            _fire.SetActive(true);
            _audioSourceAttack.Play();
            other.transform.LookAt(transform);
            other.GetComponent<Hero>().Death(transform);
        }
    }


}
