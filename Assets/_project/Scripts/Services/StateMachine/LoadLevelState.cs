using System.Collections;
using UnityEngine;

public class LoadLevelState : IPayloadState<LevelEnum>
{
    private readonly StateMachine _stateMachine;
    private readonly ISceenLoader _sceneLoader;
    private readonly IAudioService _audioService;
    private readonly IUIFactory _uiFactory;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IAdvertisementService _advertisementService;
    private readonly IProgressService _progressService;
    private readonly IAnalyticService _analyticService;

    public LoadLevelState(StateMachine stateMachine, ISceenLoader sceneLoader, IAudioService audioService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner,
        IAdvertisementService advertisementService, IProgressService progressService, IAnalyticService analyticService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _audioService = audioService;
        _uiFactory = uiFactory;
        _coroutineRunner = coroutineRunner;
        _advertisementService = advertisementService;
        _progressService = progressService;
        _analyticService = analyticService;
    }

    public void Enter(LevelEnum level)
    {
        _audioService.PlayBackMusic(SoundEnum.RegularLoopMusic);

        if (_uiFactory.Blackout == null)
            _uiFactory.CreateBlackout();

        _coroutineRunner.StartCoroutine(DaybreakWithDelay(level.ToString()));

        _analyticService.LevelStart(level);
    }

    private IEnumerator DaybreakWithDelay(string level)
    {
        yield return new WaitForSeconds(0.5f);
        _sceneLoader.Load(level);

        yield return new WaitForSeconds(0.5f);
        _uiFactory.Blackout?.Daybreak();
        _stateMachine.Enter<GameLoopState>();
    }

    public void Exit()
    {
        if(_progressService.CanShowAd())
            _advertisementService.ShowInterstitialAd();
    }
}