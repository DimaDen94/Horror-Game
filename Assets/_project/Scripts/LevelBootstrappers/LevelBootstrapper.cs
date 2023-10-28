using System.Collections;
using UnityEngine;
using Zenject;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] protected Vector3 _heroStartPosition;
    [SerializeField] protected Vector3 _heroStartRotation;
    [SerializeField] protected LevelEnum _nextLevel;
    [SerializeField] protected ExitDoor _exitDoor;
    [SerializeField] protected GameObject _startCamera;

    protected IAudioService _audioService;
    protected StateMachine _stateMachine;
    protected Hero _hero;
    private IInputService _inputService;
    private IUIFactory _uiFactory;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IInputService inputService, IUIFactory uiFactory, IAudioService audioService,
        StateMachine stateMachine,IGameFactory gameFactory) {
        _inputService = inputService;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;
    }

    protected void Start()
    {
        _startCamera.SetActive(false);
        InitHero();
        if (_exitDoor != null)
            _exitDoor.ExitDoorOpened += OnExitDoorOpened;
    }

    private void OnExitDoorOpened()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        _stateMachine.Enter<LevelCompletedState, LevelEnum>(_nextLevel);
    }

    private void InitHero()
    {
        Hud hud = _uiFactory.CreateGameHud();
        hud.Construct(_stateMachine,_audioService);
        _hero = _gameFactory.CreateHero(_heroStartPosition, Quaternion.Euler(_heroStartRotation));
        _hero.Construct(hud, _inputService, _audioService,_stateMachine);
    }

    private void OnDestroy()
    {
        if (_exitDoor != null)
            _exitDoor.ExitDoorOpened -= OnExitDoorOpened;
    }
}
