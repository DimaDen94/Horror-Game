using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowHeroTransition : Transition
{
    public void Follow() {
        NeedTransit = true;
    }
}
