using System;
using System.Collections.Generic;
using System.Linq;

public class ProgressService : IProgressService
{
    public event Action LevelIncremented;
    public event Action HintStateChanged;
    public event Action MemoryStateChanged;
    public event Action ShowAdStateChanged;

    private UserProgress _userProgress;
    private IJsonConvertor _jsonConvertor;
    private IPlayerPrefsService _playerPrefsService;
    private ILevelConfigHolder _levelConfigHolder;


    public ProgressService(IJsonConvertor jsonConvertor, IPlayerPrefsService playerPrefsService, ILevelConfigHolder levelConfigService)
    {
        _jsonConvertor = jsonConvertor;
        _playerPrefsService = playerPrefsService;
        _levelConfigHolder = levelConfigService;
        LoadProgress();
    }

    public LevelEnum LevelCompletedCount => _userProgress.CurrentLevel;

    public event Action PurchaseDataChanged;

    public void LoadProgress()
    {
        UserProgress defaultProgress = GenerateStartProgress();
        string defaultSerializeProgress = _jsonConvertor.SerializeObject(defaultProgress);
        string progressString = _playerPrefsService.GetProgress(defaultSerializeProgress);
        _userProgress = _jsonConvertor.DeserializeObject<UserProgress>(progressString);
    }

    private UserProgress GenerateStartProgress()
    {
        return new UserProgress(LevelEnum.Level1, _levelConfigHolder.Configs);
    }

    public void SetNewCurrentLevel(LevelEnum level)
    {
        _userProgress.CurrentLevel = level;
        SaveProgress();
    }

    public LevelEnum GetCurrentLevel() => _userProgress.CurrentLevel;

    public void ResetProgress()
    {
        _userProgress.CurrentLevel = LevelEnum.Level1;
        SaveProgress();
    }

    private void SaveProgress()
    {
        string progressString = _jsonConvertor.SerializeObject(_userProgress);
        _playerPrefsService.SaveProgress(progressString);
    }

    public bool IsLanguageInstalled()
    {
        return _userProgress.IsLanguageInstalled;
    }

    public void SetLanguageInstalled()
    {
        _userProgress.SetLanguageInstalled();
        SaveProgress();
    }

    public bool GetHintStates(LevelEnum level, HintEnum hint)
    {
        var _hints = _userProgress.LevelHintStates;
        LevelHintState levelStates = _hints.Find(l => l.level == level);
        if (levelStates == null)
            return false;

        HintState state = levelStates.hintStates.Find(h => h.hint == hint);
        if (state == null)
            return false;

        return state.enable;
    }

    public void SetHintActive(LevelEnum level, HintEnum hint)
    {
        List<LevelHintState> _hints = _userProgress.LevelHintStates;
        LevelHintState levelStates = _hints.Find(l => l.level == level);
        HintState state = levelStates.hintStates.Find(h => h.hint == hint);
        state.enable = true;
        SaveProgress();
        HintStateChanged?.Invoke();
    }

    public void SetMemoryActive(LevelEnum level)
    {
        List<LevelMemoryState> _memoryStates = _userProgress.LevelMemoryStates;
        LevelMemoryState levelStates = _memoryStates.Find(l => l.level == level);
        levelStates.enable = true;
        SaveProgress();
        MemoryStateChanged?.Invoke();
    }

    public bool GetMemoryActive(LevelEnum level)
    {
        List<LevelMemoryState> _memoryStates = _userProgress.LevelMemoryStates;
        LevelMemoryState levelStates = _memoryStates.Find(l => l.level == level);
        if (levelStates != null)
            return levelStates.enable;
        return false;
    }

    public int GetMemoryProgress() => (_userProgress.LevelMemoryStates.Where(item => item.enable)).Count();

    public void PurchaseProduct(string producId)
    {
        _userProgress.DisableAd();
        SaveProgress();
        ShowAdStateChanged?.Invoke();
    }

    public bool CanShowAd() => _userProgress.CanShowAd;
}