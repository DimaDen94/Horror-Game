using System;
using UnityEngine;
using DG.Tweening;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2.5f;
    [SerializeField] private HeroSlot _slot;
    [SerializeField] private HeroMover _mover;

    private Camera _camera;
    private Hud _hud;

    private InteractionObject _currentInteractionObject;

    private Vector3 _centerPosition;
    private IInputService _inputService;

    public void Construct(Hud hud, Camera camera, IInputService inputService)
    {
        _camera = camera;
        _hud = hud;
        _centerPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        _inputService = inputService;
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
                _slot.Drop();
                _slot.Put((LiftedThing)_currentInteractionObject);
                _currentInteractionObject = null;
            }
            else if (_currentInteractionObject is InteractionObject)
            {
                _currentInteractionObject.TryUse(_slot);
            }
        }
        if (_inputService.IsDropButton()) {
            _slot.Drop();

        }
    }

    public void Death(Transform killer)
    {
        _mover.Lock();
        transform.DOLookAt(killer.position, 1);
    }

    void OnDrawGizmosSelected()
    {
        if (_camera != null)
        {
            Gizmos.color = Color.red;
            Vector3 direction = _camera.transform.TransformDirection(Vector3.forward) * _interactionDistance;
            Gizmos.DrawRay(_camera.transform.position, direction);
        }
    }

    private void FindInteractionObject()
    {
        Ray ray = _camera.ScreenPointToRay(_centerPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.distance < _interactionDistance)
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
}
