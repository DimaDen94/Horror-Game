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
    private IProgressService _progressService;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IInputService inputService, IUIFactory uiFactory, IAudioService audioService,
        StateMachine stateMachine, IProgressService progressService, IGameFactory gameFactory) {
        _inputService = inputService;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _stateMachine = stateMachine;
        _progressService = progressService;
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
        _uiFactory.CreateBlackout();
        StartCoroutine(LoadNextLevelAfterDelay(0.5f));
    }

    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _progressService.SetNewCurrentLevel(_nextLevel);
        _stateMachine.Enter<LoadLevelState, string>(_nextLevel.ToString());
    }

    private void InitHero()
    {
        Hud hud = _uiFactory.CreateGameHud();
        _hero = _gameFactory.CreateHero(_heroStartPosition, Quaternion.Euler(_heroStartRotation));
        _hero.Construct(hud, _inputService, _audioService,_stateMachine, _heroStartPosition, _heroStartRotation);
    }

    private void OnDestroy()
    {
        if (_exitDoor != null)
            _exitDoor.ExitDoorOpened -= OnExitDoorOpened;
    }
}
