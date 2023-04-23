using UnityEngine;
using Zenject;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] private HeroMover _heroMover;
    private IInputService _inputService;
    private IUIFactory _uiFactory;
    private IAudioService _audioService;
    private Camera _camera;

    [Inject]
    private void Construct(IInputService inputService, IUIFactory uiFactory, IAudioService audioService) {
        _inputService = inputService;
        _uiFactory = uiFactory;
        _audioService = audioService;
    }
    private void Start()
    {
        _camera = Camera.main;
        Hud hud = _uiFactory.CreateGameHud();
        
        _heroMover.Construct(_inputService, _camera);
        _heroMover.GetComponent<Hero>().Construct(hud, _camera, _inputService, _audioService);
    }

}
