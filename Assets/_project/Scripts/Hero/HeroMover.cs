using UnityEngine;

public class HeroMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [Space(10)]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _topCameraClamp = 90.0f;
    [SerializeField] private float _bottomCameraClamp = -90.0f;


    private float _cinemachineTargetPitch;
    private float _rotationVelocity;
    
    private IInputService _inputService;
    private Camera _camera;

    public void Construct(IInputService inputService, Camera camera) {
        _inputService = inputService;
        _camera = camera;
    }

    private void Update()
    {
        Move();
    }
    private void LateUpdate()
    {
        CameraRotation();
    }
    private void Move()
    {
        Vector3 movementVector = Vector3.zero;

        if (_inputService.MoveAxis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = new Vector3(_inputService.MoveAxis.x, 0.0f, _inputService.MoveAxis.y);
            movementVector.y = 0;
            movementVector.Normalize();
        }
        if (_inputService.MoveAxis != Vector2.zero)
            movementVector = transform.right * _inputService.MoveAxis.x + transform.forward * _inputService.MoveAxis.y;

        movementVector += Physics.gravity;
        _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
    }

    private void CameraRotation()
    {
        if (_inputService.LookAxis.sqrMagnitude >= Constants.Epsilon)
        {
            _cinemachineTargetPitch += _inputService.LookAxis.y * _rotationSpeed * Time.deltaTime;
            _rotationVelocity = _inputService.LookAxis.x * _rotationSpeed * Time.deltaTime;

            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomCameraClamp, _topCameraClamp);

            _camera.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    
}
