using System.Collections;
using UnityEngine;

public class MemoryState : IState
{
    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IUIFactory _uiFactory;
    private ICoroutineRunner _coroutineRunner;
    private IImageLoader _imageLoader;
    private ILocalizationService _localizationService;
    private IProgressService _progressService;
    private ILevelConfigHolder _levelConfigHolder;

    private MemoryMenu _memoryMenu;
    private const string MemoryImagesPath = "MemoriesImages/";
    private float DestroyDelay = 1;

    public MemoryState(StateMachine stateMachine, IAudioService audioService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner,
    IImageLoader imageLoader, ILocalizationService localizationService, IProgressService progressService, ILevelConfigHolder levelConfigHolder)
    {
        _stateMachine = stateMachine;
        _audioService = audioService;
        _uiFactory = uiFactory;
        _coroutineRunner = coroutineRunner;
        _imageLoader = imageLoader;
        _localizationService = localizationService;
        _progressService = progressService;
        _levelConfigHolder = levelConfigHolder;
       
    }

    public void Enter()
    {
        LevelEnum currentLevel = _progressService.GetCurrentLevel();
        _progressService.SetMemoryActive(currentLevel);

        LevelConfig levelConfig = _levelConfigHolder.GetLevelConfig(currentLevel);
        _memoryMenu = _uiFactory.CreateMemoryMenu();
        _memoryMenu.Construct(_audioService, _stateMachine);
        _memoryMenu.SetText(_localizationService.GetTranslateByKey(levelConfig.TextMemoryKey));
        _memoryMenu.SetSprite(_imageLoader.LoadFromResource(MemoryImagesPath + currentLevel.ToString()));
        _memoryMenu.SetMemoryProgress(_progressService.GetMemoryProgress(), _levelConfigHolder.Configs.Count);

        Time.timeScale = 0;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        _memoryMenu.Hide();
        _coroutineRunner.StartCoroutine(DestroyPanel());
    }

    private IEnumerator DestroyPanel()
    {
        yield return new WaitForSeconds(DestroyDelay);
        if (_memoryMenu != null)
            _memoryMenu.DestroyMenu();
    }
}