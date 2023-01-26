using UnityEngine;

public class HeroMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [Space(10)]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _topCameraClamp = 90.0f;
    [SerializeField] private float _bottomCameraClamp = -90.0f;

    [Space(10)]
    [SerializeField] private float _jumpHeight = 1.2f;
    [SerializeField] private float _gravity = -15.0f;
    [SerializeField] private float _groundedRadius = 0.5f;
    [SerializeField] private float _groundedOffset = -0.14f;
    [SerializeField] private LayerMask GroundLayers;
    [SerializeField] private float JumpTimeout = 0.1f;
    [SerializeField] private float FallTimeout = 0.15f;

    public bool _grounded = true;

    private float _cinemachineTargetPitch;
    private float _rotationVelocity;

    private float _verticalVelocity;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private float _terminalVelocity = 53.0f;
    
    private IInputService _inputService;
    private Camera _camera;

    public void Construct(IInputService inputService, Camera camera) {
        _inputService = inputService;
        _camera = camera;
    }

    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
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
        _characterController.Move(_movementSpeed * movementVector * Time.deltaTime + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
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


    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z);
        _grounded = Physics.CheckSphere(spherePosition, _groundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }
    private void JumpAndGravity()
    {
        if (_grounded)
        {
            _fallTimeoutDelta = FallTimeout;
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }
            Debug.Log(_inputService.IsJumpButton());
            if (_inputService.IsJumpButton() && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            _jumpTimeoutDelta = JumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            //_input.jump = false;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }
    }
}
