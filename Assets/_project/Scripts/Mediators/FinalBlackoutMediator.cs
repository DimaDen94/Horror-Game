using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalBlackoutMediator : BlackoutMediator
{
    private const int FadeDuration = 2;
    private const string GooglePlayStoreURL = "https://play.google.com/store/apps/details?id=com.life_is_game.dangeon_of_fear"; 

    [SerializeField] private TextMeshProUGUI _toBeContinueText;
    [SerializeField] private TextMeshProUGUI _authorsText;
    [SerializeField] private TextMeshProUGUI _wrightComment;
    [SerializeField] private TextMeshProUGUI _thanksForPlaying;
    [SerializeField] private Button _market;

    private ILocalizationService _localizationService;

    public void Construct(ILocalizationService localizationService) {
        _localizationService = localizationService;
    }

    private void Start()
    {
        InitTexts();
        CreateAnimation();

        _market.onClick.AddListener(OpenMarket);
    }

    private void InitTexts()
    {
        _toBeContinueText.GetComponent<TextMeshProTranslator>().Construct(_localizationService);
        _authorsText.GetComponent<TextMeshProTranslator>().Construct(_localizationService);
        _wrightComment.GetComponent<TextMeshProTranslator>().Construct(_localizationService);
        _thanksForPlaying.GetComponent<TextMeshProTranslator>().Construct(_localizationService);
    }

    private void CreateAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        SetTransparentColor(_toBeContinueText);
        SetTransparentColor(_authorsText);
        sequence.Append(_toBeContinueText.DOFade(1, FadeDuration));
        sequence.Append(_toBeContinueText.DOFade(0, FadeDuration));
        sequence.Append(_authorsText.DOFade(1, FadeDuration));
    }

    private void OpenMarket() => OpenGooglePlayStore();

    public void OpenGooglePlayStore() => Application.OpenURL(GooglePlayStoreURL);

    private void SetTransparentColor(TextMeshProUGUI toBeContinueText)
    {
        var color = toBeContinueText.color;
        color.a = 0;
        toBeContinueText.color = color;
    }
}
