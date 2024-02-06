using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelBootstrapper : MonoBehaviour
{
    [SerializeField] protected Vector3 _heroStartPosition;
    [SerializeField] protected Vector3 _heroStartRotation;
    [SerializeField] protected LevelEnum _nextLevel;
    [SerializeField] protected ExitDoor _exitDoor;
    [SerializeField] protected GameObject _startCamera;
    [SerializeField] protected CollectionItem _collectionMemoryItem;

    protected Hero _hero;

    protected StateMachine _stateMachine;
    protected IAudioService _audioService;
    protected IToastService _toastService;
    protected IUIFactory _uiFactory;
    protected IInAppReviewService _inAppReviewService;
    protected IAnalyticService _analyticService;
    private IAccessLayer _accessLayer;
    [SerializeField] private List<LiftedThing> _liftedThings;

    private IInputService _inputService;
    private IGameFactory _gameFactory;
    private IProgressService _progressService;


    [Inject]
    private void Construct(IInputService inputService, IUIFactory uiFactory, IAudioService audioService, StateMachine stateMachine, IGameFactory gameFactory,
        IProgressService progressService, IToastService toastService, IInAppReviewService inAppReviewService, IAnalyticService analyticService, IAccessLayer accessLayer) {
        _inputService = inputService;
        _uiFactory = uiFactory;
        _audioService = audioService;
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _toastService = toastService;
        _inAppReviewService = inAppReviewService;
        _analyticService = analyticService;
        _accessLayer = accessLayer;
    }

    protected void Start()
    {
        _startCamera.SetActive(false);

        InitHero();
        InitExitDoor();

        if (CanShowHint())
            ShowHint();
        _progressService.HintStateChanged += ShowHint;

        InitCollectedItem();
    }


    private void InitExitDoor()
    {
        if (_exitDoor != null)
        {
            _exitDoor.Construct(_toastService);
            _exitDoor.ExitDoorOpened += OnExitDoorOpened;
        }
    }


    private bool CanShowHint()
    {
        return _progressService.GetHintStates(_progressService.GetCurrentLevel(), HintEnum.HintHighlight);
    }

    private void InitCollectedItem()
    {
        if (_collectionMemoryItem == null)
            return;

        if (_progressService.GetMemoryActive(_progressService.GetCurrentLevel()))
            _collectionMemoryItem.gameObject.SetActive(false);
        else
            _collectionMemoryItem.ItemCollected += OnCollectedMemory;
    }

    private void ShowHint()
    {
        if (_progressService.GetHintStates(_progressService.GetCurrentLevel(), HintEnum.HintHighlight))
            ShowHighlight();
        if (_progressService.GetHintStates(_progressService.GetCurrentLevel(), HintEnum.EnemySlowDown))
            EnemySlowDown();
    }

    protected virtual void EnemySlowDown() {}

    private void ShowHighlight()
    {
        foreach (var liftedThing in _liftedThings)
        {
            var light = _gameFactory.CreateLight(liftedThing.transform);
            liftedThing.SetLight(light);
        }
    }

    private void OnExitDoorOpened()
    {
        StopLevel();
        LoadNextLevel();
    }

    protected virtual void StopLevel() {}

    private void OnCollectedMemory() => _stateMachine.Enter<MemoryState>();

    private void LoadNextLevel() => _stateMachine.Enter<LevelCompletedState, LevelEnum>(_nextLevel);

    private void InitHero()
    {
        Hud hud = _uiFactory.CreateGameHud();
        hud.Construct(_stateMachine,_audioService, _accessLayer);
        _hero = _gameFactory.CreateHero(_heroStartPosition, Quaternion.Euler(_heroStartRotation));
        _hero.Construct(hud, _inputService, _audioService,_stateMachine);
    }

    protected void OnDestroy()
    {
        _progressService.HintStateChanged -= ShowHint;

        if (_exitDoor != null)
            _exitDoor.ExitDoorOpened -= OnExitDoorOpened;
    }

}
