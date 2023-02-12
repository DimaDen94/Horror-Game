using UnityEngine;

public interface IInputService 
{
    Vector2 MoveAxis { get; }
    Vector2 LookAxis { get; }

    bool IsActionButton();
}
