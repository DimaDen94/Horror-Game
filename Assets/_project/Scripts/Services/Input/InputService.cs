

using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    protected const string Action = "Action";

    public abstract Vector2 Axis { get; }
    public bool IsActionButtonUp() => SimpleInput.GetButtonUp(Action);

    protected Vector2 SimpleInputAxis() => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
}
