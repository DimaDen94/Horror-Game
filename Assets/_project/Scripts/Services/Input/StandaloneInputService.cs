using UnityEngine;
public class StandaloneInputService : InputService
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public override Vector2 MoveAxis
    {
        get
        {
            Vector2 axis = SimpleMoveInputAxis();
            if (axis == Vector2.zero)
                axis = UnityMoveAxis();
            return axis;
        }
    }

    public override Vector2 LookAxis
    {
        get
        {
            Vector2 axis = SimpleLookInputAxis();
            if (axis == Vector2.zero)
                axis = UnityLookAxis();
            return axis;
        }
    }

    private Vector2 UnityMoveAxis() => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    private Vector2 UnityLookAxis() => new Vector2(Input.GetAxis(MouseX), Input.GetAxis(MouseY));

}
