using System;

public interface IProgressService
{
    event Action HintStateChanged;

    LevelEnum GetCurrentLevel();
    void LoadProgress();
    void ResetProgress();
    void SetNewCurrentLevel(LevelEnum level);
    bool IsLanguageInstalled();
    void SetLanguageInstalled();
    bool GetHintStates(LevelEnum level, HintEnum hint);
    void SetHintActive(LevelEnum level, HintEnum hint);
}