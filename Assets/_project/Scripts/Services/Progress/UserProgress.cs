using System;

public class UserProgress
{
    private LevelEnum _currentLevel;
    private bool _isLanguageInstalled = false;

    public LevelEnum CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    public bool IsLanguageInstalled => _isLanguageInstalled;

    public void SetLanguageInstalled() => _isLanguageInstalled = true;
}