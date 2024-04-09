using System;
using UnityEngine;

public class AccessLayer : IAccessLayer
{
    private IProgressService _progressService;
    private IAdvertisementService _advertisementService;
    private IIAPService _iapService;
    private IToastService _toastService;
    private IAnalyticService _analyticService;
    private StateMachine _stateMachine;

    public event Action AccessChanged;

    public AccessLayer(IProgressService progressService, IAdvertisementService advertisementService, IIAPService iapService, IToastService toastService,
        IAnalyticService analyticService, StateMachine stateMachine)
    {
        _progressService = progressService;
        _advertisementService = advertisementService;
        _iapService = iapService;
        _toastService = toastService;
        _analyticService = analyticService;
        _stateMachine = stateMachine;
    }

    public void OnAdCheckboxClick()
    {
        if (!_progressService.CanShowAd())
            return;

        _iapService.StartPurchase(PurchaseItemType.DisableAdvertising);
    }

    public void OnHintCheckBoxClick(HintEnum hintType)
    {
        if (_progressService.GetHintStates(_progressService.GetCurrentLevel(), hintType))
            return;

        _progressService.SetHintActive(_progressService.GetCurrentLevel(), hintType);
        _analyticService.HintUnlock(_progressService.GetCurrentLevel(), hintType);

    }

    public void TryOpenHintMenu()
    {
        if (_progressService.GetHintStates(_progressService.GetCurrentLevel(), HintEnum.HintText) || !_progressService.CanShowAd())
        {
            _stateMachine.Enter<HintMenuState>();
            return;
        }

        if (_advertisementService.CanShowRewardedAd())
            _advertisementService.ShowRewardedAd(() =>
            {
                _progressService.SetHintActive(_progressService.GetCurrentLevel(), HintEnum.HintText);
                _stateMachine.Enter<HintMenuState>();
                _analyticService.HintUnlock(_progressService.GetCurrentLevel(), HintEnum.HintText);
            });
        else
        {
            _toastService.ShowToast(TranslatableKey.AdvertisingIsNotReady);
            _advertisementService.LoadRewardedAd();
        }
    }
}