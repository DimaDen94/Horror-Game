using UnityEngine;
using Zenject;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] private HeroMover _heroMover;
    private IInputService _inputService;
    private IUIFactory _uiFactory;
    private Camera _camera;

    [Inject]
    private void Construct(IInputService inputService, IUIFactory uiFactory) {
        _inputService = inputService;
        _uiFactory = uiFactory;
    }
    private void Start()
    {
        _camera = Camera.main;
        _uiFactory.CreateHud();
        _heroMover.Construct(_inputService, _camera);
        _heroMover.GetComponent<Hero>().Construct(_uiFactory.Hud.GetComponent<Hud>(), _camera, _inputService);
    }

}
