using System;
using UnityEngine;

public interface ILocalizationService
{
    Sprite GetCurrentLanguageIcon();
    SystemLanguage GetCurrentLanguageType();
    string GetTranslateByKey(TranslatableKey key);
    void SaveLanguage(SystemLanguage lang);
    event Action<SystemLanguage> LanguageChanged;

    string GetTranslateByKey(string stringKey);
}