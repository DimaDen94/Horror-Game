using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobAdvertisementService : IAdvertisementService
{

#if UNITY_ANDROID
    private string _interstitialAdId = "ca-app-pub-4425759167125218/1497654199";
    private string _rewardedAdId = "ca-app-pub-4425759167125218/1624317713";
#elif UNITY_IPHONE
    private string _adInterstitialId = "ca-app-pub-4425759167125218/1497654199";
    private string _rewardedAdId = "ca-app-pub-4425759167125218/1624317713";
#endif

    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    public AdMobAdvertisementService()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_interstitialAdId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_rewardedAdId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public bool CanShowRewardedAd() {
        return _rewardedAd != null && _rewardedAd.CanShowAd();
    }

    public void ShowRewardedAd(Action success)
    {
        if (CanShowRewardedAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                success.Invoke();
                LoadRewardedAd();
            });
        }
        else {
            LoadRewardedAd();
        }
    }
}