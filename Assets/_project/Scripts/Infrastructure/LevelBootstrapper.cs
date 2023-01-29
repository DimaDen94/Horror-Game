using UnityEngine;
using Zenject;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] private HeroMover _heroMover;
    private IInputService _inputService;
    private Camera _camera;

    [Inject]
    private void Construct(IInputService inputService) {
        _inputService = inputService;
    }
    private void Start()
    {
        _camera = Camera.main;
        _heroMover.Construct(_inputService, _camera);
    }

    
}
