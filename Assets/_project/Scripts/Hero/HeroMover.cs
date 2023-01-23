using UnityEngine;

public class HeroMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;
    private IInputService _inputService;
    private Camera _camera;

    public void Construct(IInputService inputService, Camera camera) {
        _inputService = inputService;
        _camera = camera;
    }

    private void Update()
    {
        Vector3 movementVector = Vector3.zero;

        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = new Vector3(_inputService.Axis.x, 0.0f, _inputService.Axis.y);
            movementVector.y = 0;
            movementVector.Normalize();
        }
        if(_inputService.Axis != Vector2.zero)
            movementVector = transform.right * _inputService.Axis.x + transform.forward * _inputService.Axis.y;

        movementVector += Physics.gravity;
        _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
    }
}
