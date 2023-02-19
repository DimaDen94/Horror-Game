

using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string HorizontalMove = "HorizontalMove";
    protected const string VerticalMove = "VerticalMove";
    protected const string HorizontalLook = "HorizontalLook";
    protected const string VerticalLook = "VerticalLook";
    protected const string Action = "Action";
    protected const string Drop = "Drop";

    public abstract Vector2 MoveAxis { get; }
    public abstract Vector2 LookAxis { get; }

    public bool IsActionButton() => SimpleInput.GetButtonDown(Action);
    public bool IsDropButton() => SimpleInput.GetButtonDown(Drop);

    protected Vector2 SimpleMoveInputAxis() => new Vector2(SimpleInput.GetAxis(HorizontalMove), SimpleInput.GetAxis(VerticalMove));
    protected Vector2 SimpleLookInputAxis() => new Vector2(SimpleInput.GetAxis(HorizontalLook), -SimpleInput.GetAxis(VerticalLook));
}
