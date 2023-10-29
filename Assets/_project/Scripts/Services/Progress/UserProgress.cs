﻿using System.Collections.Generic;

public class UserProgress
{
    private LevelEnum _currentLevel;
    private List<LevelHintState> _levelHintStates;
    private bool _isLanguageInstalled = false;

    public UserProgress(LevelEnum currentLevel, List<LevelConfig> levelConfigs)
    {
        _currentLevel = currentLevel;
        _levelHintStates = new List<LevelHintState>();

        foreach (LevelConfig item in levelConfigs)
        {
            _levelHintStates.Add(new LevelHintState(item.level, item.hints));
        }
    }

    public UserProgress() {
    }

    public LevelEnum CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    public bool IsLanguageInstalled => _isLanguageInstalled;

    public List<LevelHintState> LevelHintStates => _levelHintStates;

    public void SetLanguageInstalled() => _isLanguageInstalled = true;
}
