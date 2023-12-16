using System;

public interface IProgressService
{
    event Action HintStateChanged;
    event Action MemoryStateChanged;
    event Action ShowAdStateChanged;

    LevelEnum GetCurrentLevel();
    void LoadProgress();
    void ResetProgress();
    void SetNewCurrentLevel(LevelEnum level);
    bool IsLanguageInstalled();
    void SetLanguageInstalled();
    bool GetHintStates(LevelEnum level, HintEnum hint);
    void SetHintActive(LevelEnum level, HintEnum hint);
    void SetMemoryActive(LevelEnum level);
    int GetMemoryProgress();
    int GetMemoryCount();
    bool GetMemoryActive(LevelEnum level);
    void PurchaseProduct(string producId);
    bool CanShowAd();
}