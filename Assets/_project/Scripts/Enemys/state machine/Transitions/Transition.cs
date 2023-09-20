using System;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public Transform Target { get;protected set; }

    public State TargetState { get => _targetState; set => _targetState = value; }

    public bool NeedTransit { get;protected set; }

    public void Init(Transform target)
    {
        Target = target;
    }

    internal void Deactivate()
    {
        NeedTransit = false;
        enabled = false;
    }
}
