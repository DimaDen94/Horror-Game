using System;
using System.Collections.Generic;

public class UserProgress
{
    private LevelEnum _currentLevel;
    private List<LevelHintState> _levelHintStates;
    private List<LevelMemoryState> _levelMemoryStates;
    private bool _isLanguageInstalled = false;
    private bool _canShowAd = true;

    public UserProgress(LevelEnum currentLevel, List<LevelConfig> levelConfigs)
    {
        _currentLevel = currentLevel;
        _levelHintStates = new List<LevelHintState>();
        _levelMemoryStates = new List<LevelMemoryState>();

        foreach (LevelConfig item in levelConfigs)
        {
            _levelHintStates.Add(new LevelHintState(item.level, item.hints));

            if (item.level != LevelEnum.Level2_5)
                _levelMemoryStates.Add(new LevelMemoryState(item.level));
        }
    }

    public UserProgress() { }

    public LevelEnum CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    public bool IsLanguageInstalled => _isLanguageInstalled;

    public List<LevelHintState> LevelHintStates { get => _levelHintStates; set => _levelHintStates = value; }
    public List<LevelMemoryState> LevelMemoryStates { get => _levelMemoryStates; set => _levelMemoryStates = value; }

    public bool CanShowAd => _canShowAd;

    public void SetLanguageInstalled() => _isLanguageInstalled = true;

    public void DisableAd() => _canShowAd = false;
}
