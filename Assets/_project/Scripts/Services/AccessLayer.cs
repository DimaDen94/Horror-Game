using System;

public class AccessLayer : IAccessLayer
{
    private IProgressService _progressService;
    private IAdvertisementService _advertisementService;
    private IIAPService _iapService;
    private IToastService _toastService;
    private IAnalyticService _analyticService;

    public event Action AccessChanged;

    public AccessLayer(IProgressService progressService, IAdvertisementService advertisementService, IIAPService iapService, IToastService toastService, IAnalyticService analyticService) {
        _progressService = progressService;
        _advertisementService = advertisementService;
        _iapService = iapService;
        _toastService = toastService;
        _analyticService = analyticService;
    }

    public void OnAdCheckboxClick()
    {
        if (!_progressService.CanShowAd())
            return;

        _iapService.StartPurchase(PurchaseItemType.DisableAdvertising.ToString());
    }

    public void OnHintClick(HintEnum hintType)
    {
        if (_progressService.GetHintStates(_progressService.GetCurrentLevel(), hintType))
            return;

        if (_progressService.CanShowAd())
        {
            if (_advertisementService.CanShowRewardedAd())
                _advertisementService.ShowRewardedAd(() => {
                    _progressService.SetHintActive(_progressService.GetCurrentLevel(), hintType);
                    _analyticService.HintUnlock(_progressService.GetCurrentLevel(), hintType);
                });
            else
            {
                _toastService.ShowToast(TranslatableKey.AdvertisingIsNotReady);
                _advertisementService.LoadRewardedAd();
            }
        }
        else
            _progressService.SetHintActive(_progressService.GetCurrentLevel(), hintType);

       
    }

}