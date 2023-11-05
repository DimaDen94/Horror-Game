public class ToastService : IToastService
{
    private IUIFactory _uiFactory;
    private ILocalizationService _localizationService;

    private ToastView _toastView;

    public ToastService(IUIFactory uiFactory, ILocalizationService localizationService)
    {
        _uiFactory = uiFactory;
        _localizationService = localizationService;
    }

    public void ShowToast(TranslatableKey key) {
        if (_toastView == null)
        {
            _toastView = _uiFactory.CreateToast();
            _toastView.SetText(_localizationService.GetTranslateByKey(key));
            _toastView.ToastDestroyed += OnToastDestroy;
        }
    }

    private void OnToastDestroy()
    {
        _toastView.ToastDestroyed -= OnToastDestroy;
        _toastView = null;
    }
}