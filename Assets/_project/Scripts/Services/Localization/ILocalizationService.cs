using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILocalizationService
{
    List<SystemLanguage> SupportedLanguages { get; }

    Sprite GetCurrentLanguageIcon();
    SystemLanguage GetCurrentLanguageType();
    void SaveLanguage(SystemLanguage lang);
    event Action<SystemLanguage> LanguageChanged;

    string GetTranslateByKey(TranslatableKey key);
    string GetTranslateByKey(string stringKey);
    Sprite GetLanguageIcon(SystemLanguage language);
}