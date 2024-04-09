using System;
using UnityEngine;

public class HeroMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    private bool _lockMovement = false;
    private bool _lockRotate = false;

    private float _cinemachineTargetPitch;
    private float _rotationVelocity;
    
    private IInputService _inputService;
    private Camera _camera;
    private float _speed;

    public float SpeedChangeRate = 1.0f;

    public void Construct(IInputService inputService, Camera camera) {
        _inputService = inputService;
        _camera = camera;
    }

    private void Update() => Move();

    private void LateUpdate() => CameraRotation();

    private void Move()
    {

        if (_lockMovement)
            return;
        Vector3 movementVector = Vector3.zero;

        SpeedCalculate();

        if (_inputService.MoveAxis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = new Vector3(_inputService.MoveAxis.x, 0.0f, _inputService.MoveAxis.y);
            movementVector.y = 0;
            movementVector.Normalize();
        }
        if (_inputService.MoveAxis != Vector2.zero)
            movementVector = transform.right * _inputService.MoveAxis.x + transform.forward * _inputService.MoveAxis.y;

        movementVector += Physics.gravity;
        _characterController.Move(_speed * movementVector * Time.deltaTime);
    }

    private void SpeedCalculate()
    {
        float targetSpeed = HeroParameters.MovementSpeed;

        if (_inputService.MoveAxis == Vector2.zero)
            targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;


        float speedOffset = 0.001f;
        float inputMagnitude = _inputService.MoveAxis.magnitude;


        if (currentHorizontalSpeed < targetSpeed - speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;

        }
        else
        {
            _speed = targetSpeed;
        }
    }

    public void LockMovement() => _lockMovement = true;

    public void UnlockMovement() => _lockMovement = false;

    public void LockRotated() => _lockRotate = true;

    public void UnlockRotated() => _lockRotate = false;

    private void CameraRotation()
    {
        if (_lockRotate)
            return;

        if (_inputService.LookAxis.sqrMagnitude >= Constants.Epsilon)
        {
            _cinemachineTargetPitch += _inputService.LookAxis.y * HeroParameters.RotationSpeed * Time.deltaTime;
            _rotationVelocity = _inputService.LookAxis.x * HeroParameters.RotationSpeed * Time.deltaTime;

            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, HeroParameters.BottomCameraClamp, HeroParameters.TopCameraClamp);

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
