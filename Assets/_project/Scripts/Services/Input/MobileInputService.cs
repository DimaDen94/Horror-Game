

using System;
using UnityEngine;

public class MobileInputService : InputService
{
    public override Vector2 MoveAxis
    {
        get
        {
            return SimpleMoveInputAxis();
        }
    }

    public override Vector2 LookAxis
    {
        get
        {
            return SimpleLookInputAxis();
        }
    }

   
}