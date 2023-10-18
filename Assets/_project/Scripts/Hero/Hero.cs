using UnityEngine;
using DG.Tweening;
using System;

public class Hero : MonoBehaviour, IHitable
{
    [SerializeField] private HeroSlot _slot;
    [SerializeField] private HeroMover _mover;
    [SerializeField] private Camera _camera;

    private Hud _hud;

    private InteractionObject _currentInteractionObject;

    private Vector3 _centerPosition;
    private IInputService _inputService;
    private IAudioService _audioService;
    private StateMachine _stateMachine;
    private Vector3 _startPosition;
    private Vector3 _startRotation;

    public void Construct(Hud hud, IInputService inputService, IAudioService audioService, StateMachine stateMachine, Vector3 startPosition, Vector3 startRotation)
    {
        _hud = hud;
        _centerPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        _inputService = inputService;
        _audioService = audioService;
        _stateMachine = stateMachine;
        _mover.Construct(_inputService, _camera);
    }

    void Update()
    {
        FindInteractionObject();
        UpdateUI();
        DoAction();
    }

    private void DoAction()
    {
        if (_inputService.IsActionButton()) {
            if (_currentInteractionObject is LiftedThing)
            {
                _audioService.PlayAudio(SoundEnum.PickUp);
                _slot.Drop();
                _slot.Put((LiftedThing)_currentInteractionObject);
                _currentInteractionObject = null;
            }
            else if (_currentInteractionObject is InteractionObject)
            {
                bool wasUsed = _currentInteractionObject.TryUse(_slot);
                if (!wasUsed){
                    _audioService.PlayAudio(SoundEnum.Wrong);
                }
            }
        }
        if (_inputService.IsDropButton()) { 
            _audioService.PlayAudio(SoundEnum.Drop);
            _slot.Drop();

        }
    }

    public void Death(Transform killer)
    {
        _mover.Lock();
        transform.DOLookAt(killer.position, 1);
        _stateMachine.Enter<DeadState>();
    }

    void OnDrawGizmosSelected()
    {
        if (_camera != null)
        {
            Gizmos.color = Color.red;
            Vector3 direction = _camera.transform.TransformDirection(Vector3.forward) * HeroParameters.InteractionDistance;
            Gizmos.DrawRay(_camera.transform.position, direction);
        }
    }

    private void FindInteractionObject()
    {
        Ray ray = _camera.ScreenPointToRay(_centerPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.distance < HeroParameters.InteractionDistance)
            if (hit.transform.GetComponent<InteractionObject>() != _slot.Thing)
                _currentInteractionObject = hit.transform.GetComponent<InteractionObject>();
        else
            _currentInteractionObject = null;
    }

    private void UpdateUI()
    {
        if (_currentInteractionObject == null)
            _hud.HideActionButton();
        else
            _hud.ShowActionButton();

        if (_slot.IsFull)
            _hud.ShowDropButton();
        else
            _hud.HideDropButton();
    }

    public void Hit(Transform striking)
    {
        Death(striking);
    }
}
