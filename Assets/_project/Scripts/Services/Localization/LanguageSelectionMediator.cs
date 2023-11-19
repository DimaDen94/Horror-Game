using System;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelectionMediator : MonoBehaviour
{
    private const string HideStateName = "Hide";

    [SerializeField] private Transform _container;
    [SerializeField] private Animator _animator;

    private List<LanguageItemView> _languageItems = new List<LanguageItemView>();

    private IAudioService _audioService;
    private ILocalizationService _localizationService;
    private IUIFactory _uiFactory;
    private StateMachine _stateMachine;

    public void Construct(IAudioService audioService, ILocalizationService localizationService, IUIFactory uiFactory, StateMachine stateMachine) {
        _audioService = audioService;
        _localizationService = localizationService;
        _uiFactory = uiFactory;
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        foreach (SystemLanguage language in _localizationService.SupportedLanguages)
        {
            LanguageItemView item = _uiFactory.CreateLanguageItemView(_container);
            item.Construct(language, _localizationService.GetLanguageIcon(language));
            item.GetComponent<ButtonClickPlayer>().Construct(_audioService);
            _languageItems.Add(item);
            item.LanguageClick += OnLanguageClick;
        }
    }

    private void OnDestroy()
    {
        foreach (LanguageItemView itemView in _languageItems)
            itemView.LanguageClick -= OnLanguageClick;
    }

    private void OnLanguageClick(SystemLanguage language)
    {
        _localizationService.SaveLanguage(language);
        _stateMachine.Enter<MainMenuState>();
    }

    public void Hide() => _animator?.Play(HideStateName);

    public void DestroyPanel() => Destroy(gameObject);
}
