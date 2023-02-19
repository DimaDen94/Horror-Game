using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]private HeroSlot _slot;

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
                _slot.Put((LiftedThing)_currentInteractionObject);
            }
            else if (_currentInteractionObject is ExitDoor) {
                ((ExitDoor)_currentInteractionObject).TryUse(_slot.Thing);
            }
        }
    }

    private void FindInteractionObject()
    {
        Ray ray = _camera.ScreenPointToRay(_centerPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.distance < 2)
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
    }
}
