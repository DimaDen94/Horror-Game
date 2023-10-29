using System;

public class ProgressService : IProgressService
{
    private UserProgress _userProgress;
    private IJsonConvertor _jsonConvertor;
    private IPlayerPrefsService _playerPrefsService;
    private ILevelConfigHolder _levelConfigHolder;

    public event Action LevelIncremented;

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

    private UserProgress GenerateStartProgress() => new UserProgress(LevelEnum.Level1, _levelConfigHolder.Configs);

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
}