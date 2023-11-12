using System;

public interface IAdvertisementService
{
    bool CanShowRewardedAd();
    void LoadInterstitialAd();
    void LoadRewardedAd();
    void ShowInterstitialAd();
    void ShowRewardedAd(Action showed);
}