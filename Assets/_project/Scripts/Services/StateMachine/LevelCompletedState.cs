using System.Collections;
using UnityEngine;

public class LevelCompletedState : IPayloadState<LevelEnum>
{
    private const string StoryImagesPath = "StoryImages/";

    private StateMachine _stateMachine;
    private IAudioService _audioService;
    private IUIFactory _uiFactory;
    private ICoroutineRunner _coroutineRunner;
    private IImageLoader _imageLoader;
    private ILocalizationService _localizationService;
    private IProgressService _progressService;

    public LevelCompletedState(StateMachine stateMachine,IAudioService audioService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner,
        IImageLoader imageLoader, ILocalizationService localizationService, IProgressService progressService)
    {
        _stateMachine = stateMachine;
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
        StoryBlackoutMediator storyBlackout = _uiFactory.CreateStoryBlackout();

        storyBlackout.SetText(_localizationService.GetTranslateByKey(currentLevel.ToString()));
        storyBlackout.SetSprite(_imageLoader.LoadFromResource(StoryImagesPath + currentLevel.ToString()));
        _audioService.PlaySpeech(currentLevel);
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
        if (nextLevel != LevelEnum.Final)
        {
            _stateMachine.Enter<LoadLevelState, LevelEnum>(nextLevel);
        }
        else
        {
            yield return new WaitForSeconds(2);
            _uiFactory.Blackout.DestroyBlackout();
            _stateMachine.Enter<GameCompletedState>();
        }
    }
}