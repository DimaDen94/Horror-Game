using System;

public interface IProgressService
{
    LevelEnum GetCurrentLevel();
    void LoadProgress();
    void ResetProgress();
    void SetNewCurrentLevel(LevelEnum level);
}