using UnityEngine;
using Zenject;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] protected HeroMover _heroMover;
    private IInputService _inputService;
    private IUIFactory _uiFactory;
    protected IAudioService _audioService;
    private StateMachine _stateMachine;
    private Camera _camera;

    [Inject]
    private void Construct(IInputService inputService, IUIFactory uiFactory, IAudioService audioService, StateMachine stateMachine) {
        _inputService = inputService;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _stateMachine = stateMachine;
    }
    private void Start()
    {
        InitHero();
    }

    protected void InitHero()
    {
        _camera = Camera.main;
        Hud hud = _uiFactory.CreateGameHud();

        _heroMover.Construct(_inputService, _camera);
        _heroMover.GetComponent<Hero>().Construct(hud, _camera, _inputService, _audioService,_stateMachine);
    }
}
