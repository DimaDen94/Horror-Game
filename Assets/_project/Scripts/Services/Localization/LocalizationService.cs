using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LocalizationService : ILocalizationService
{
    private const string LANGUAGE_KEY = "LANGUAGE_KEY";
    private const SystemLanguage _defaultLang = SystemLanguage.English;
    private const string ContentPath = "Localization/Content/";
    private const string IconsPath = "Localization/Icons/";
    private IProgressService _progressService;
    private IAssetProvider _assetProvider;
    private IImageLoader _imageLoader;

    private List<SystemLanguage> _supportedLanguages = new List<SystemLanguage>() {
        SystemLanguage.English,
        //SystemLanguage.Spanish,
        //SystemLanguage.Italian,
        //SystemLanguage.Russian,
        //SystemLanguage.French,
        //SystemLanguage.German,
        //SystemLanguage.Ukrainian,
        //SystemLanguage.Polish
    };

    private LanguageContent _languageContent;
    private List<SystemLanguage> _systemLanguages;

    public LocalizationService(IProgressService progressService, IAssetProvider assetProvider, IImageLoader imageLoader)
    {
        _progressService = progressService;
        _assetProvider = assetProvider;
        _imageLoader = imageLoader;
        LoadLanguage();
    }


    public event Action<SystemLanguage> LanguageChanged;


    private void LoadLanguage()
    {
        SystemLanguage lang;
        _systemLanguages = GetAllLanguages();
        if (!_progressService.IsLanguageInstalled() && _supportedLanguages.Contains(Application.systemLanguage))
        {
            lang = Application.systemLanguage;
            Debug.Log("systemLanguage " + lang.ToString());
            SaveLanguage(lang);
            _progressService.SetLanguageInstalled();
        }
        else
        {
            lang = GetCurrentLanguage();
            LanguageChanged?.Invoke(lang);
            _languageContent = _assetProvider.LoadScriptableObject<LanguageContent>(ContentPath + lang.ToString());
        }
    }

    private List<SystemLanguage> GetAllLanguages()
    {
        return Enum.GetValues(typeof(SystemLanguage)).Cast<SystemLanguage>().ToList();
    }

    private SystemLanguage GetCurrentLanguage()
    {
        int index = PlayerPrefs.GetInt(LANGUAGE_KEY, _systemLanguages.IndexOf(_defaultLang));
        return _systemLanguages[index];
    }

    public Sprite GetCurrentLanguageIcon()
    {
        SystemLanguage lang = GetCurrentLanguage();
        return _imageLoader.LoadFromResource(IconsPath + lang.ToString());
    }
    public SystemLanguage GetCurrentLanguageType()
    {
        return GetCurrentLanguage();
    }
    public void SaveLanguage(SystemLanguage lang)
    {
        PlayerPrefs.SetInt(LANGUAGE_KEY, _systemLanguages.IndexOf(lang));
        _languageContent = _assetProvider.LoadScriptableObject<LanguageContent>(ContentPath + lang.ToString());
        LanguageChanged?.Invoke(lang);
    }

    public string GetTranslateByKey(TranslatableKey key)
    {
        if (string.IsNullOrEmpty(_languageContent.GetValueByKey(key)))
            return key.ToString();
        return _languageContent.GetValueByKey(key);
    }

    public string GetTranslateByKey(string stringKey)
    {
        foreach (TranslatableKey key in Enum.GetValues(typeof(TranslatableKey)).Cast<TranslatableKey>())
        {
            if (key.ToString().Equals(stringKey))
                return GetTranslateByKey(key);
        }
        return string.Empty;
    }
}
