using UnityEngine;

public class GameLoopState : IState
{
    private IAdvertisementService _advertisementService;
    private IProgressService _progressService;

    public GameLoopState(IAdvertisementService advertisementService, IProgressService progressService)
    {
        _advertisementService = advertisementService;
        _progressService = progressService;
    }

    public void Enter()
    {
        Time.timeScale = 1;
        if (_progressService.CanShowAd())
        {
            _advertisementService.LoadInterstitialAd();

            if(!_advertisementService.CanShowRewardedAd())
                _advertisementService.LoadRewardedAd();
        }
       
    }

    public void Exit()
    {
        
    }
}