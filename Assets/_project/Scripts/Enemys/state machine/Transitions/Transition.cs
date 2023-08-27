using System;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public Hero Target { get;protected set; }

    public State TargetState { get => _targetState; set => _targetState = value; }

    public bool NeedTransit { get;protected set; }

    public void Init(Hero target)
    {
        Target = target;
    }

    internal void Deactivate()
    {
        NeedTransit = false;
        enabled = false;
    }
}
