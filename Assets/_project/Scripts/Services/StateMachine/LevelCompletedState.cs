using System.Collections;
using UnityEngine;

public class LevelCompletedState : IPayloadState<LevelEnum>
{
    private const string StoryImagesPath = "StoryImages/";

    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IAudioService _audioService;
    private IUIFactory _uiFactory;
    private ICoroutineRunner _coroutineRunner;
    private IImageLoader _imageLoader;
    private ILocalizationService _localizationService;
    private IProgressService _progressService;

    public LevelCompletedState(StateMachine stateMachine, ISceenLoader sceneLoader, IAudioService audioService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner,
        IImageLoader imageLoader, ILocalizationService localizationService, IProgressService progressService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _audioService = audioService;
        _uiFactory = uiFactory;
        _coroutineRunner = coroutineRunner;
        _imageLoader = imageLoader;
        _localizationService = localizationService;
        _progressService = progressService;
    }

    public void Enter(LevelEnum nextLevel)
    {
        LevelEnum currentLevel = _progressService.GetCurrentLevel();

        StoryBlackoutMediator storyBlackotu = _uiFactory.CreateStoryBlackout();
        storyBlackotu.SetText(_localizationService.GetTranslateByKey(currentLevel.ToString()));
        storyBlackotu.SetSprite(_imageLoader.LoadFromResource(StoryImagesPath + currentLevel.ToString()));

        _sceneLoader.Load(nextLevel.ToString());
        _audioService.PlayBackMusic(SoundEnum.RegularLoopMusic);
        _progressService.SetNewCurrentLevel(nextLevel);

        _coroutineRunner.StartCoroutine(DaybreakWithDelay(nextLevel));
    }

    public void Exit()
    {
        
    }

    private IEnumerator DaybreakWithDelay(LevelEnum nextLevel)
    {
        yield return new WaitForSeconds(3);
        _stateMachine.Enter<LoadLevelState, LevelEnum>(nextLevel);
    }
}