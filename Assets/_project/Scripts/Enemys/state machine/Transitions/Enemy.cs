using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy: MonoBehaviour
{
    private const float _slowSpeedMultiplier = 0.75f;

    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private float _startSpeed;

    public Animator Animator => _animator;

    private void Awake()
    {
        if(_agent != null)
            _startSpeed = _agent.speed;
    }

    public void SlowDown()
    {
        _agent.speed = _startSpeed * _slowSpeedMultiplier;
    }
}