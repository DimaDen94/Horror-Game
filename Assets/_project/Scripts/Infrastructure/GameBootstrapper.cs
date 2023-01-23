using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private HeroMover _heroMover;
    private IInputService _inputService;
    private Camera _camera;
    private void Awake()
    {
        RegisterInputService();
    }
    private void Start()
    {
        _camera = Camera.main;
        _heroMover.Construct(_inputService, _camera);
    }

    private void RegisterInputService()
    {
        if (Application.isEditor)
            _inputService = new StandaloneInputService();
        else
            _inputService = new MobileInputService();
    }
}
