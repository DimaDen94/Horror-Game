using System;

public interface IProgressService
{
    event Action HintStateChanged;
    event Action MemoryStateChanged;

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
    bool GetMemoryActive(LevelEnum level);
}