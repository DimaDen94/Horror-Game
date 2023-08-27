using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    [SerializeField]public Hero Target {get;protected set;}


    public void Enter(Hero target) {
        Target = target;
        enabled = true;
        foreach (var transition in _transitions) {
            transition.enabled = true;
            transition.Init(Target);
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit) {
                return transition.TargetState;
            }
        }

        return null;
    }
    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.Deactivate();
            }
            enabled = false;
        }
    }
}
