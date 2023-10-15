using System.Collections;
using UnityEngine;
using Zenject;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] protected HeroMover _heroMover;
    [SerializeField] protected LevelEnum _nextLevel;
    [SerializeField] protected ExitDoor _exitDoor;

    private IInputService _inputService;
    private IUIFactory _uiFactory;
    protected IAudioService _audioService;
    protected StateMachine _stateMachine;
    private IProgressService _progressService;
    private Camera _camera;
    

    [Inject]
    private void Construct(IInputService inputService, IUIFactory uiFactory, IAudioService audioService, StateMachine stateMachine, IProgressService progressService) {
        _inputService = inputService;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _stateMachine = stateMachine;
        _progressService = progressService;
    }

    protected void Start()
    {
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
        _camera = Camera.main;
        Hud hud = _uiFactory.CreateGameHud();

        _heroMover.Construct(_inputService, _camera);
        _heroMover.GetComponent<Hero>().Construct(hud, _camera, _inputService, _audioService,_stateMachine);
    }

    private void OnDestroy()
    {
        if (_exitDoor != null)
            _exitDoor.ExitDoorOpened -= OnExitDoorOpened;
    }
}
